using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ArtificialGravityBody : MonoBehaviour, ISpawned
{
    [SerializeField] private ArtificialGravityAttractor _ground;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody= GetComponent<Rigidbody> ();
        _rigidbody.freezeRotation= true;
    }

    private void FixedUpdate()
    {
        if (_ground != null)
        {
            _ground.Attract(_rigidbody, transform);
        }
    }

    public void Init(ArtificialGravityAttractor ground)
    {
        _ground = ground;
    }
}
