using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SurvivorMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _distanceToTarget;

    private Transform _target;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            Move();
            Rotate_transform();
            // Rotate_MoveRotation();
           // Rotate_rotation();
        }
    }

    public void SetTarget(Transform target)
    {
        _target= target;
    }

    private void Move()
    {
        if (Vector3.Distance(_target.position, transform.position) >= _distanceToTarget)
        {
            Vector3 direction = _target.position - transform.position.normalized;
            _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, _target.position, _speedMovement * Time.deltaTime);
        }
    }

    private void Rotate_rotation()
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, _target.position - transform.position);
        _rigidbody.rotation= rotation*_rigidbody.rotation;
    }

    private void Rotate_MoveRotation()
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, _target.position - transform.position);
        _rigidbody.MoveRotation(rotation);
    }

    private void Rotate_transform()
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, _target.position - transform.position);
        transform.rotation = rotation * transform.rotation;
    }
}
