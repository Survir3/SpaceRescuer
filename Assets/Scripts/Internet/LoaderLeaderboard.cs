using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoaderLeaderboard : MonoBehaviour
{
    [SerializeField] private SaverData _saverData;
    [SerializeField] private CreatorLevelConfig _creatorLevelConfig;

    private Texture2D _textureLeader;
    private List<LeaderPlayerInfo> _leaderEntriesInfo = new List<LeaderPlayerInfo>();
    private bool _isCorrutineDownloadPhotoFinished = false;

    public event UnityAction<IReadOnlyList<LeaderPlayerInfo>> IsLoadLeadersFinish;
    public event UnityAction<int> IsLoadUserRank;

    public IReadOnlyList<LeaderPlayerInfo> LeaderEntriesInfo => _leaderEntriesInfo;
    public int BestResult { get; private set; }

    private void OnEnable()
    {
        _creatorLevelConfig.ChangeValuePoints += OnChangeValuePoints;
    }

    private void OnDisable()
    {
        _creatorLevelConfig.ChangeValuePoints -= OnChangeValuePoints;
    }

    private void Start()
    {
        LoadEntries();
    }

    public void LoadEntries()
    {
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.GetPlayerEntry(ConstantsString.Leaderboard, DownloadBestResult);
            Leaderboard.GetEntries(ConstantsString.Leaderboard, StartSetLeadersEntriesInfo);
        }
    }

    public void OnChangeValuePoints(int points)
    {
        if (PlayerAccount.IsAuthorized)
        {
            if(points > BestResult)
            {
                BestResult = points;
            }

            Leaderboard.GetPlayerEntry(ConstantsString.Leaderboard, SetScore);
        }
    }

    private void SetScore(LeaderboardEntryResponse leaderboardEntry)
    {
        int minScore = 1;

        if (leaderboardEntry.score < BestResult && BestResult>= minScore)
        {
            Leaderboard.SetScore(ConstantsString.Leaderboard, BestResult);
        }
    }

    private void StartSetLeadersEntriesInfo(LeaderboardGetEntriesResponse entries)
    {
        StartCoroutine(SetLeadersEntriesInfo(entries.entries));
    }

    private IEnumerator SetLeadersEntriesInfo(LeaderboardEntryResponse[] entry)
    {
        for (int i = 0; i < entry.Length; i++)
        {
            int score = entry[i].score;
            string name = entry[i].player.publicName;
            string urlTexture = entry[i].player.profilePicture;

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
            _leaderEntriesInfo.Add(newLeaderPlayerInfo);
            _textureLeader = null;
        }

        IsLoadLeadersFinish?.Invoke(LeaderEntriesInfo);
    }

    private void DownloadBestResult(LeaderboardEntryResponse entry)
    {
        BestResult = entry.score;
        IsLoadUserRank?.Invoke(BestResult);
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