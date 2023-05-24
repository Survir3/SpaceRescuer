using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ArtificialGravityAttractor), typeof(SphereCollider))]
public abstract class Spawner : Pool, IIncreaseForLevel
{
    [SerializeField] protected List<GameObject> _prefabs;
    [SerializeField] protected float _delay;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] private SleeperSpawner _sleeperSpawner;

    protected Coroutine _pauseSpawn;
    protected float _radius;
    protected Vector3 _scaleBody;
    protected float _timerSpawn;

    public int CountAdded { get; private set; } = 0;

    public event UnityAction<int> IsAdded;
    public event UnityAction IsAllAdded;
    public event UnityAction<Spawner> IsSpawned;

    private void Awake()
    {        
        _radius = transform.localScale.x / 2;
        _scaleBody = _prefabs[0].transform.localScale / 2;
        Init(_prefabs);
    }

    protected virtual void Spawned()
    {
        _timerSpawn += Time.deltaTime;

        if (_timerSpawn >= _delay)
        {
            Vector3 newPosition = GetSpawnedPosition();

            if (_pauseSpawn != null)
                return;
           
           if(TryGetObject(out GameObject gameObject))
            {
                gameObject.transform.position = newPosition;
                gameObject.transform.parent= null;
                gameObject.SetActive(true);
                CollisionHandler newHandler = gameObject.GetComponentInChildren<CollisionHandler>();
                newHandler.Added+= OnAdded;
                _timerSpawn = 0;
                IsSpawned?.Invoke(this);
            }
        }
    }

    protected Vector3 GetSpawnedPosition()
    {
        Vector3 newPosition = GetSpawnRandomPosition();
        RaycastHit[] hits = GetAllObstacles(newPosition);
        int countTry = 0;
        int maxCountTry = 1000;

        while (hits.Length > 1)
        {
            if(countTry>=maxCountTry)
            {
               _pauseSpawn= StartCoroutine(_sleeperSpawner.Countdown());
            }
            else
            {
            newPosition = GetSpawnRandomPosition();
            hits = GetAllObstacles(newPosition);
            countTry++;
            }
        }

        return newPosition;
    }

    private Vector3 GetSpawnRandomPosition()
    {
        var rq= transform.position + Random.insideUnitSphere.normalized * _radius;
        return rq;
    }

    private RaycastHit[] GetAllObstacles(Vector3 position)
    {
        float maxDistance = 0;
        var halfBox = new Vector3(0.5f, 0.5f, 0.5f);

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
