using IJunior.TypedScenes;
using UnityEngine;

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

    protected override void SpawnedToStart(int count)
    {
        while (TryGetObject(out GameObject prefab))
        {
            Vector3 newPosition = GetSpawnedPosition();
            ActivePrefab(prefab, newPosition);
        }
    }
}
