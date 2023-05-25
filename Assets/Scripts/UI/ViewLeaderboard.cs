using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour, ISceneLoadHandler<IReadOnlyList<LeaderPlayerInfo>>
{
    [SerializeField] private List<ViewLeader> _viewLeaders;

    public void OnSceneLoaded(IReadOnlyList<LeaderPlayerInfo> argument)
    {
        if (argument != null)
        {
            for (int i = 0; i < argument.Count; i++)
            {
                _viewLeaders[i].InitWithTexture(argument[i]);

                Debug.Log(argument[i].Name);
            }
        }
        else
        {
            Debug.Log(argument);
        }
    }
}
