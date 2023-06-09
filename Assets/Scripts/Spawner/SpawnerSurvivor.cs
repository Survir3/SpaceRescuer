using IJunior.TypedScenes;

public class SpawnerSurvivor : Spawner, ISceneLoadHandler<DataLoadScene>
{
    private void Update()
    {
        Spawned();
    }

    public void OnSceneLoaded(DataLoadScene argument)
    {
        _count = argument.LevelConfig.CountSurvivorsToLevel;
    }
}
