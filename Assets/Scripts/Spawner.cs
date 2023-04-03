using System.Collections;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected ArtificialGravityBody _gravityBody;
    [SerializeField] protected float _delay;
    [SerializeField] protected int _maxCount;

    private ArtificialGravityAttractor _attractor;
    private Coroutine _currentCoroutine;
    private int _currentCount;

    private void Awake()
    {
        _attractor = GetComponent<ArtificialGravityAttractor>();
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
        ArtificialGravityBody newBody = Instantiate(_gravityBody, transform.position + Random.insideUnitSphere, Quaternion.identity);       
        newBody.Init(_attractor);
        _currentCount++;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_delay);
        Spawned();
        _currentCoroutine = null;
    }
}
