using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ArtificialGravityBody : MonoBehaviour
{
    [SerializeField] private ArtificialGravityAttractor _ground;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody= GetComponent<Rigidbody> ();
        _rigidbody.freezeRotation= true;
    }
    private void Update()
    {
        _ground.Attract(_rigidbody, transform);
    }
}
