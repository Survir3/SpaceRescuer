using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour, ISceneLoadHandler<DataLoadScene>
{
    [SerializeField] private List<ViewLeader> _viewLeaders;
    [SerializeField] private LoaderLeaderboard _leaderboard;

    private void OnEnable()
    {
        _leaderboard.IsLoadFinish += ShowLeaderPlayer;
    }

    private void OnDisable()
    {
        _leaderboard.IsLoadFinish -= ShowLeaderPlayer;
    }

    public void OnSceneLoaded(DataLoadScene argument)
    {
        IReadOnlyList<LeaderPlayerInfo> leaderPlayerInfo = argument.LeaderPlayers;

        ShowLeaderPlayer(leaderPlayerInfo);
    }

    private void ShowLeaderPlayer(IReadOnlyList<LeaderPlayerInfo> leaderPlayerInfo)
    {

        if (leaderPlayerInfo != null)
        {
            for (int i = 0; i < _viewLeaders.Count; i++)
            {
                if (_viewLeaders[i] == null || leaderPlayerInfo[i] == null)
                    return;

                _viewLeaders[i].InitWithTexture(leaderPlayerInfo[i]);
            }
        }
    }
}
