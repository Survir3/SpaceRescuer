using IJunior.TypedScenes;
using UnityEngine;

public class SpawnerSurvivor : Spawner, ISceneLoadHandler<DataLoadScene>
{
    private void Update()
    {
        Spawned();
    }

    public void OnSceneLoaded(DataLoadScene argument)
    {
       // Debug.Log("Info " + argument.LevelConfig == null);
        _count = argument.LevelConfig.CountSurvivorsToLevel;
    }
}
