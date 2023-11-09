using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ArtificialGravityAttractor), typeof(SphereCollider))]
public abstract class Spawner : Pool
{
    [SerializeField] protected List<GameObject> Templet;
    [SerializeField] protected float Delay;
    [SerializeField] protected LayerMask LayerMask;

    protected float Radius;
    protected Vector3 ScaleBody;
    protected float TimerSpawn;

    private int _halfRatio = 2;

    public event UnityAction<int> IsAdded;
    public event UnityAction IsAllAdded;
    public event UnityAction<Spawner> IsSpawned;

    public int CountAdded { get; private set; } = 0;
    public bool IsAllAdd => Count == CountAdded;

    private void Awake()
    {
        Radius = transform.localScale.x / _halfRatio;
        ScaleBody = Templet[0].transform.localScale / _halfRatio;
        Init(Templet);
    }

    protected virtual void SpawnedToDelay()
    {
        TimerSpawn += Time.deltaTime;

        if (TimerSpawn >= Delay)
        {
            Vector3 newPosition = GetSpawnedPosition();

            if (TryGetObject(out GameObject prefab))
            {
                ActivePrefab(prefab, newPosition);
            }
        }
    }

    protected virtual void SpawnedToStart(int count)
    {
        for (int i = 0; i < count; i++)
        {
            TryGetObject(out GameObject prefab);
            Vector3 newPosition = GetSpawnedPosition();
            ActivePrefab(prefab, newPosition);
        }
    }

    protected void ActivePrefab(GameObject prefab, Vector3 newPosition)
    {
        prefab.transform.position = newPosition;
        prefab.transform.parent = null;
        prefab.SetActive(true);
        CollisionHandler newHandler = prefab.GetComponentInChildren<CollisionHandler>();
        newHandler.Added += OnAdded;
        TimerSpawn = 0;
        IsSpawned?.Invoke(this);
    }

    protected Vector3 GetSpawnedPosition()
    {
        Vector3 newPosition = GetSpawnRandomPosition();
        RaycastHit[] hits = GetAllObstacles(newPosition);

        int maxCountRaycastHit = 1;

        while (hits.Length > maxCountRaycastHit)
        {
            newPosition = GetSpawnRandomPosition();
            hits = GetAllObstacles(newPosition);
        }

        return newPosition;
    }

    protected void OnAdded(CollisionHandler collisionHandler)
    {
        collisionHandler.Added -= OnAdded;
        CountAdded++;
        IsAdded?.Invoke(CountAdded);

        if (CountAdded == Count)
            IsAllAdded?.Invoke();
    }

    private Vector3 GetSpawnRandomPosition()
    {
        return transform.position + Random.insideUnitSphere.normalized * Radius;
    }

    private RaycastHit[] GetAllObstacles(Vector3 position)
    {
        float maxDistance = 0;
        float lengthEdge = 0.5f;
        var halfBox = new Vector3(lengthEdge, lengthEdge, lengthEdge);

        return Physics.BoxCastAll(position, halfBox, Vector3.forward, Quaternion.identity, maxDistance, LayerMask);
    }
}
