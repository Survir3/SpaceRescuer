using IJunior.TypedScenes;

public class SpawnerEnemy : Spawner, ISceneLoadHandler<LevelConfig>
{    
    private void Start()
    {
        SpawnedToStart(Count);
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        _count = argument.CountEnemy;
    }
}
