using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour, ISceneLoadHandler<IReadOnlyList<LeaderPlayerInfo>>
{
    [SerializeField] private List<ViewLeader> _viewLeaders;
    [SerializeField] private LoaderLeaderboard _loaderLeaderboard;

    public void OnSceneLoaded(IReadOnlyList<LeaderPlayerInfo> argument)
    {
        if (argument != null)
        {
            for (int i = 0; i < _loaderLeaderboard.LeaderPlayersInfo.Count; i++)
            {
                _viewLeaders[i].InitWithTexture(_loaderLeaderboard.LeaderPlayersInfo[i]);
            }
        }
    }
}
