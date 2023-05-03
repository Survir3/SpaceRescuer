using UnityEngine;

public class SpawnerSurvivor : Spawner
{
    private void Update()
    {
        Spawned(_prefabs[0]);
    }
}
