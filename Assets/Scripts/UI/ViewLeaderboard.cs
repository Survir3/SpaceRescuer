using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour, ISceneLoadHandler<DataLoadScene>
{
    [SerializeField] private List<ViewLeader> _viewLeaders;

    public void OnSceneLoaded(DataLoadScene argument)
    {
        Debug.Log(99);

        if (argument != null)
        {
            for (int i = 0; i < argument.LeaderPlayers.Count; i++)
            {
                _viewLeaders[i].InitWithTexture(argument.LeaderPlayers[i]);
            }
        }

        Debug.Log(98);
    }
}
