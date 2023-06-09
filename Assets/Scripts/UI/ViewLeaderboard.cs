using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour, ISceneLoadHandler<DataLoadScene>
{
    [SerializeField] private List<ViewLeader> _viewLeaders;

    public void OnSceneLoaded(DataLoadScene argument)
    {
        if (argument != null)
        {
            for (int i = 0; i < argument.LeaderPlayers.Count; i++)
            {
                _viewLeaders[i].InitWithTexture(argument.LeaderPlayers[i]);
            }
        }
    }
}
