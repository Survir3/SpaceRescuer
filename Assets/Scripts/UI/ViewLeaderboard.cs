using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour, ISceneLoadHandler<DataLoadScene>
{
    [SerializeField] private List<ViewLeader> _viewLeaders;

    public void OnSceneLoaded(DataLoadScene argument)
    {
        Debug.Log("LeaderPlayers.Count " + argument.LeaderPlayers.Count);

        if (argument != null)
        {
            for (int i = 0; i < _viewLeaders.Count; i++)
            {
                _viewLeaders[i].InitWithTexture(argument.LeaderPlayers[i]);
            }
        }

        Debug.Log("_viewLeaders.Count " + _viewLeaders.Count);
    }
}
