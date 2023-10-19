using IJunior.TypedScenes;
using UnityEngine;

public class EnemySpawner : Spawner, ISceneLoadHandler<LevelConfig>
{
    private void Start()
    {
        Spawned();
    }

    protected override void Spawned()
    {
        while (TryGetObject(out GameObject prefab))
        {
            Vector3 newPosition = GetSpawnedPosition();

            ActivePrefab(prefab, newPosition);
        }
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        _count = argument.LevelConfig.CountEnemy;
    }
}
