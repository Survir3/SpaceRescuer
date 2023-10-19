using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoaderLeaderboard : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private ConnecterYandex _connecterYandex;

    private Texture2D _textureLeader;
    private List<LeaderPlayerInfo> _leaderPlayersInfo = new List<LeaderPlayerInfo>();
    private bool _isCorrutineDownloadPhotoFinished=false;
    private int _countLeaderPlayerInfo = 5;
    private bool _isLoaderCorrectLoad = false;

    public bool IsLoaderCorrectLoad => _isLoaderCorrectLoad;
    public IReadOnlyList<LeaderPlayerInfo> LeaderPlayerInfos => _leaderPlayersInfo;

    public event UnityAction<IReadOnlyList<LeaderPlayerInfo>> IsLoadFinish;

    private void OnEnable()
    {
        if (_player != null && _spawnerSurvivor!=null)
        {
            _player.IsDie += OnUpdateScore;
            _spawnerSurvivor.IsAllAdded += OnUpdateScore;
        }

       // if (_connecterYandex != null)
          //  _connecterYandex.ReadyLoadedLeaderboard += LoadEntries;
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            _spawnerSurvivor.IsAllAdded -= OnUpdateScore;
            _player.IsDie -= OnUpdateScore;
        }

       // if (_connecterYandex != null)
           // _connecterYandex.ReadyLoadedLeaderboard -= LoadEntries;
    }

    public void LoadEntries()
    {
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.GetEntries(ConstantsString.Leaderboard, StartSetLeadersPlayersInfo, null, _countLeaderPlayerInfo);
        }
        else
        {
            IsLoadFinish?.Invoke(null);
        }
    }

    private void OnUpdateScore()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
            Leaderboard.GetPlayerEntry(ConstantsString.Leaderboard, SetScore);
#endif
    }

    private void SetScore(LeaderboardEntryResponse leaderboardEntry)
    {

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

            while (!_isCorrutineDownloadPhotoFinished)
            {
                yield return null;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = ConstantsString.Incognito;
            }

            var newLeaderPlayerInfo = new LeaderPlayerInfo();
            newLeaderPlayerInfo.Init(name, score, _textureLeader);
            _leaderPlayersInfo.Add(newLeaderPlayerInfo);
            _textureLeader = null;
        }

        _isLoaderCorrectLoad = true;
        
        IsLoadFinish?.Invoke(LeaderPlayerInfos);
    }

    private IEnumerator DownloadPhoto(string url)
    {

        var remoteImage = new RemoteImage(url);
        remoteImage.Download();

            while (!remoteImage.IsDownloadFinished)
        {
            _isCorrutineDownloadPhotoFinished = remoteImage.IsDownloadFinished;
            yield return null;
        }

        if (remoteImage.IsDownloadSuccessful)            
            _textureLeader = remoteImage.Texture;

        _isCorrutineDownloadPhotoFinished = remoteImage.IsDownloadFinished;
    }
}
