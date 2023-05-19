using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderLeaderboard : MonoBehaviour
{
    [SerializeField] private Player _player;    

    private Texture2D _textureLeader;
    private List<LeaderPlayerInfo> _leaderPlayersInfo = new List<LeaderPlayerInfo>();
    private bool _isCorrutineDownloadFinished=false;

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
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.GetEntries(ConstantsString.Leaderboard, StartSetLeadersPlayersInfo);
        }
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

    private void StartSetLeadersPlayersInfo(LeaderboardGetEntriesResponse entries)
    {
        StartCoroutine(SetLeadersPlayersInfo(entries));
    }

    private IEnumerator SetLeadersPlayersInfo(LeaderboardGetEntriesResponse entries)
    {
        for (int i = 0; i < entries.entries.Length; i++)
        {
            int score = entries.entries[i].score;
            string name = entries.entries[i].player.publicName;
            string urlTexture = entries.entries[i].player.profilePicture;

            StartCoroutine(DownloadPhoto(urlTexture));

            if (!_isCorrutineDownloadFinished)
            {
                yield return null;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = "Incognito";
            }

            var newLeaderPlayerInfo = new LeaderPlayerInfo();
            newLeaderPlayerInfo.Init(name, score, _textureLeader);
            _leaderPlayersInfo.Add(newLeaderPlayerInfo);
            _textureLeader = null;
        }
    }

    private IEnumerator DownloadPhoto(string url)
    {

        var remoteImage = new RemoteImage(url);
        remoteImage.Download();

            while (!remoteImage.IsDownloadFinished)
        {
            _isCorrutineDownloadFinished = remoteImage.IsDownloadFinished;
            yield return null;
        }

        if (remoteImage.IsDownloadSuccessful)            
            _textureLeader = remoteImage.Texture;

        _isCorrutineDownloadFinished = remoteImage.IsDownloadFinished;
    }
}
