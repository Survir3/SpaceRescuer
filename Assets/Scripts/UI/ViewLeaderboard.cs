using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour
{
    [SerializeField] private List<ViewLeader> _viewLeaders;
    [SerializeField] private LoaderLeaderboard _loaderLeaderboard;

    private void OnEnable()
    {
        for (int i = 0; i < _loaderLeaderboard.LeaderPlayersInfo.Count; i++)
        {
            _viewLeaders[i].InitWithTexture(_loaderLeaderboard.LeaderPlayersInfo[i]);
        }
    }
}
