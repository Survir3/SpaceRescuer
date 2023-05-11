using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoaderLeaderboard : MonoBehaviour
{
    [SerializeField] private Player _player;

    private List<LeaderPlayerInfo> _leaderPlayersInfo = new List<LeaderPlayerInfo>();
    private Texture _loadedTexture;

    public IReadOnlyList<LeaderPlayerInfo> LeaderPlayersInfo => _leaderPlayersInfo;

    private void OnEnable()
    {
        if (_player != null)
            _player.IsDie += OnUpdateScore;
    }

    private void OnDisable()
    {
        if (_player != null)
            _player.IsDie -= OnUpdateScore;
    }

    private void Start()
    {
        if (!PlayerAccount.IsAuthorized)
            return;

        Leaderboard.GetEntries(ConstantsString.Leaderboard, (result) =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                int score = result.entries[i].score;
                string name = result.entries[i].player.publicName;
                string urlTexture= result.entries[i].player.profilePicture;
                Debug.Log(urlTexture);
                StartCoroutine(DownloadPhoto(urlTexture));
                
                if(string.IsNullOrEmpty(name))
                {
                    name = "Incognito";
                }

                var newLeaderPlayerInfo = new LeaderPlayerInfo();
                newLeaderPlayerInfo.Init(name, score, _loadedTexture);

                _leaderPlayersInfo.Add(newLeaderPlayerInfo);
            }
        });
    }

    private void OnUpdateScore()
    {
        Leaderboard.GetPlayerEntry(ConstantsString.Leaderboard, SetScore);
    }

    private void SetScore(LeaderboardEntryResponse leaderboardEntry)
    {
        Debug.Log("SetScore");

        if (leaderboardEntry == null)
        {
            Leaderboard.SetScore(ConstantsString.Leaderboard, _player.Points.Value);
            return;
        }

        if (leaderboardEntry.score < _player.Points.Value)
        {
            Leaderboard.SetScore(ConstantsString.Leaderboard, _player.Points.Value);
        }
    }

    private IEnumerator DownloadPhoto(string url)
    {
        UnityWebRequest result = UnityWebRequestTexture.GetTexture(url);
        yield return result.SendWebRequest();

        if(result.result ==UnityWebRequest.Result.ConnectionError || result.result == UnityWebRequest.Result.ProtocolError)
        {
           _loadedTexture = null; 
        }
        else
        {
            _loadedTexture = ((DownloadHandlerTexture)result.downloadHandler).texture;
        }
    }
}
