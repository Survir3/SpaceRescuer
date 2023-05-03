using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private void Start()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            Instantiate(GetRandomPrefab(), GetSpawnedPosition(), Quaternion.identity).Init(_attractor);
        }
    }
}
