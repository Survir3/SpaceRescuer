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
        Debug.Log("Инфо " + argument.LevelConfig == null);
        _count = argument.LevelConfig.CountSurvivorsToLevel;
    }
}
