using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoaderLeaderboard : MonoBehaviour
{
    [SerializeField] private Points _points;
    [SerializeField] private ConnecterYandex _connecterYandex;

    private Texture2D _textureLeader;
    private List<LeaderPlayerInfo> _leaderPlayersInfo = new List<LeaderPlayerInfo>();
    private bool _isCorrutineDownloadPhotoFinished=false;
    private int _countLeaderPlayerInfo = 5;

    public IReadOnlyList<LeaderPlayerInfo> LeaderPlayerInfos => _leaderPlayersInfo;

    public event UnityAction<IReadOnlyList<LeaderPlayerInfo>> IsLoadFinish;

    private void OnEnable()
    {
        if (_points != null)
            _points.ChangeValue += OnUpdateScore;

        if (_connecterYandex != null)
            _connecterYandex.IsConnect += LoadEntries;
    }

    private void OnDisable()
    {
        if (_points != null)
            _points.ChangeValue -= OnUpdateScore;

        if (_connecterYandex != null)
            _connecterYandex.IsConnect -= LoadEntries;
    }

    private void LoadEntries()
    {        
        Leaderboard.GetEntries(ConstantsString.Leaderboard, StartSetLeadersPlayersInfo, ErrorString, _countLeaderPlayerInfo);
    }

    private void OnUpdateScore(int value)
    {
        Leaderboard.GetPlayerEntry(ConstantsString.Leaderboard, SetScore);
    }

    private void SetScore(LeaderboardEntryResponse leaderboardEntry)
    {
        if (leaderboardEntry == null)
        {
            Leaderboard.SetScore(ConstantsString.Leaderboard, _points.Value);
            return;
        }

        if (leaderboardEntry.score < _points.Value)
        {
            Leaderboard.SetScore(ConstantsString.Leaderboard, _points.Value);
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
                name = "Incognito";
            }

            var newLeaderPlayerInfo = new LeaderPlayerInfo();
            newLeaderPlayerInfo.Init(name, score, _textureLeader);
            _leaderPlayersInfo.Add(newLeaderPlayerInfo);
            _textureLeader = null;
        }

        Debug.Log(_leaderPlayersInfo.Count);
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

    private void ErrorString(string callBack)
    {
        Debug.LogError("ErrorString " + callBack);
    }
}
