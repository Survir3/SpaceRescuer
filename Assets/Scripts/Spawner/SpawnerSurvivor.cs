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
        _count = argument.LevelConfig.CountSurvivorsToLevel;
        Debug.LogError("Info argument.LevelConfig ==_count? " + argument.LevelConfig.CountSurvivorsToLevel);
    }
}
