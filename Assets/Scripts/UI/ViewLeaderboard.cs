using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour
{
    [SerializeField] private List<ViewLeader> _viewLeaders;
    [SerializeField] private LoaderLeaderboard _leaderboard;

    private void OnEnable()
    {
        _leaderboard.IsLoadLeadersFinish += ShowLeaderEntries;
    }

    private void OnDisable()
    {
        _leaderboard.IsLoadLeadersFinish -= ShowLeaderEntries;
    }

    private void ShowLeaderEntries(IReadOnlyList<LeaderPlayerInfo> leaderPlayerInfo)
    {
        if (leaderPlayerInfo != null && _viewLeaders != null)
        {
            int minCountList = Mathf.Min(leaderPlayerInfo.Count, _viewLeaders.Count);

            for (int i = 0; i < minCountList; i++)
            {
                    _viewLeaders[i].InitWithTexture(leaderPlayerInfo[i]);
                    _viewLeaders[i].gameObject.SetActive(true);
            }
        }
    }
}
