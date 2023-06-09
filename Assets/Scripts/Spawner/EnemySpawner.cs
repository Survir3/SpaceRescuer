using IJunior.TypedScenes;
using UnityEngine;

public class EnemySpawner : Spawner, ISceneLoadHandler<DataLoadScene>
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

    public void OnSceneLoaded(DataLoadScene argument)
    {
        _count = argument.LevelConfig.CountEnemy;
    }
}
