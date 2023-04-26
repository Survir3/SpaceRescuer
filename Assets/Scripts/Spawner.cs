using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ArtificialGravityAttractor), typeof(SphereCollider))]
public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected ArtificialGravityBody _gravityBody;
    [SerializeField] protected float _delay;
    [SerializeField] protected int _maxCount;

    private ArtificialGravityAttractor _attractor;
    private Coroutine _currentCoroutine;
    private int _currentCount;
    private float _radius;
    private Vector3 _scaleBody;

    private void Awake()
    {
        _attractor = GetComponent<ArtificialGravityAttractor>();
        _radius = transform.localScale.x/2;
        _scaleBody= _gravityBody.transform.localScale/2;
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Spawned();
        }
    }

    private void Update()
    {
        if(_currentCoroutine==null && _currentCount<_maxCount)
        {
            _currentCoroutine=StartCoroutine(Timer());
        }
    }

    protected void Spawned()
    {
        Vector3 newPosition = GetSpawnedPosition();

        ArtificialGravityBody newBody = Instantiate(_gravityBody, newPosition, Quaternion.identity);

        newBody.Init(_attractor);
        _currentCount++;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_delay);
        Spawned();
        _currentCoroutine = null;
    }

    private Vector3 GetSpawnedPosition()
    {
        Vector3 newPosition = GetSpawnRandomPosition();
        RaycastHit[] hits = GetAllObstacles(newPosition);

        while (hits.Length>1)
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
}
