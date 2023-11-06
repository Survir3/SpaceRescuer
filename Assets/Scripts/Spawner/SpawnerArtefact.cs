using IJunior.TypedScenes;

public class SpawnerArtefact : Spawner, ISceneLoadHandler<LevelConfig>
{
    private void Update()
    {
        SpawnedToDelay();
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        _count = argument.CountArtefact;
    }
}
