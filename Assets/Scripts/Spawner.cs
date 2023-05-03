using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArtificialGravityAttractor), typeof(SphereCollider))]
public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected List<ArtificialGravityBody> _prefabs;
    [SerializeField] protected float _delay;
    [SerializeField] protected int _maxCount;

    protected ArtificialGravityAttractor _attractor;
    protected Coroutine _currentCoroutine;
    protected int _currentCount;
    protected float _radius;
    protected Vector3 _scaleBody;
    protected float _timerSpawn;

    private void Awake()
    {
        _attractor = GetComponent<ArtificialGravityAttractor>();
        _radius = transform.localScale.x / 2;
        _scaleBody = _prefabs[0].gameObject.transform.localScale / 2;
    }

    protected void Spawned(ArtificialGravityBody prefab)
    {
        _timerSpawn += Time.deltaTime;

        if (_timerSpawn >= _delay)
        {
            Vector3 newPosition = GetSpawnedPosition();
            ArtificialGravityBody newBody = Instantiate(prefab, newPosition, Quaternion.identity);
            newBody.Init(_attractor);
            CollisionHandler newHandler = newBody.GetComponentInChildren<CollisionHandler>();
            newHandler.Added+= OnAdded;
            _currentCount++;
            _timerSpawn = 0;
        }
    }

    protected ArtificialGravityBody GetRandomPrefab()
    {
        return _prefabs[Random.Range(0, _prefabs.Count)];
    }

    protected Vector3 GetSpawnedPosition()
    {
        Vector3 newPosition = GetSpawnRandomPosition();
        RaycastHit[] hits = GetAllObstacles(newPosition);

        while (hits.Length > 2)
        {
            newPosition = GetSpawnRandomPosition();
            hits = GetAllObstacles(newPosition);
        }

        return newPosition;
    }

    private Vector3 GetSpawnRandomPosition()
    {
        return transform.position + Random.insideUnitSphere.normalized * _radius;
    }

    private RaycastHit[] GetAllObstacles(Vector3 position)
    {
        return Physics.BoxCastAll(position, _scaleBody, Vector3.forward);
    }

    private void OnAdded(CollisionHandler collisionHandler)
    {
        _currentCount--;
        collisionHandler.Added -= OnAdded;
    }

}
