using UnityEngine;

[RequireComponent(typeof(Survivor))]
public class SurvivorMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;
    private Transform _target;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody= GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            Move();
        }
    }

    public void SetTarget(Transform target)
    {
        _target= target;
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _target.position.normalized * _speedMovement * Time.deltaTime);
        Debug.Log(_target.position.normalized);
    }
}
