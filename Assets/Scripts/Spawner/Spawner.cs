using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(ArtificialGravityAttractor), typeof(SphereCollider))]
public abstract class Spawner : Pool, IIncreaseForLevel
{
    [SerializeField] protected List<GameObject> _prefabs;
    [SerializeField] protected float _delay;
    [SerializeField] protected LayerMask _layerMask;

    protected float _radius;
    protected Vector3 _scaleBody;
    protected float _timerSpawn;

    private int _halfRatio = 2;

    public int CountAdded { get; private set; } = 0;
    public bool IsAllAdd => Count == CountAdded;

    public event UnityAction<int> IsAdded;
    public event UnityAction IsAllAdded;
    public event UnityAction<Spawner> IsSpawned;

    private void Awake()
    {        
        _radius = transform.localScale.x / _halfRatio;
        _scaleBody = _prefabs[0].transform.localScale / _halfRatio;
        Init(_prefabs);
    }

    protected virtual void Spawned()
    {
        _timerSpawn += Time.deltaTime;

        if (_timerSpawn >= _delay)
        {
            Vector3 newPosition = GetSpawnedPosition();
           
           if(TryGetObject(out GameObject prefab))
            {
                ActivePrefab(prefab, newPosition);
            }
        }
    }

    protected void ActivePrefab(GameObject prefab, Vector3 newPosition)
    {
        prefab.transform.position = newPosition;
        prefab.transform.parent = null;
        prefab.SetActive(true);
        CollisionHandler newHandler = prefab.GetComponentInChildren<CollisionHandler>();
        newHandler.Added += OnAdded;
        _timerSpawn = 0;
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

    private Vector3 GetSpawnRandomPosition()
    {
        return transform.position + Random.insideUnitSphere.normalized * _radius;        
    }

    private RaycastHit[] GetAllObstacles(Vector3 position)
    {
        float maxDistance = 0;
        float lengthEdge = 0.5f;
        var halfBox = new Vector3(lengthEdge, lengthEdge, lengthEdge);

        return Physics.BoxCastAll(position, halfBox, Vector3.forward, Quaternion.identity, maxDistance, _layerMask);
    }

    protected void OnAdded(CollisionHandler collisionHandler)
    {
        collisionHandler.Added -= OnAdded;
        CountAdded++;
        IsAdded?.Invoke(CountAdded);

        if (CountAdded == Count)
            IsAllAdded?.Invoke();
    }

    public void SetValueToStartLevel(float value)
    {
        _count = (int)value;
    }
}
