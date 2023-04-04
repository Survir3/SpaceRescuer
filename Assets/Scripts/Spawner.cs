using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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
        _radius = GetComponent<Transform>().localScale.x/2;
        _scaleBody= _gravityBody.transform.localScale/2;
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
        Vector3 newPosition = transform.position + Random.insideUnitSphere.normalized * _radius;
        RaycastHit[] hits = Physics.BoxCastAll(newPosition, _scaleBody, Vector3.forward);

        while (hits.Length>1)
        {
            newPosition = transform.position + Random.insideUnitSphere.normalized * _radius;
            hits = Physics.BoxCastAll(newPosition, _scaleBody, Vector3.forward);
        }

        return newPosition;
    }
}
