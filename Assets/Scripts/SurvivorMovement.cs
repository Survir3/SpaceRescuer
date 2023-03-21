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

    private void Update()
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
        _rigidbody.AddForce(_target.position*_speedMovement*Time.deltaTime);
    }
}
