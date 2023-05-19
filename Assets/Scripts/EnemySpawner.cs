using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private void Start()
    {
        Spawned();
    }

    protected override void Spawned()
    {
        while (TryGetObject(out GameObject gameObject))
        {
            Vector3 newPosition = GetSpawnedPosition();

            gameObject.transform.position = newPosition;
            gameObject.transform.parent = null;
            gameObject.SetActive(true);
            CollisionHandler newHandler = gameObject.GetComponentInChildren<CollisionHandler>();
            newHandler.Added += OnAdded;
        }
    }
}
