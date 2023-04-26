using UnityEngine;

public abstract class Movement : MonoBehaviour, IMultiplied
{
    [SerializeField] protected float _speedMovement;
    [SerializeField] protected float _speedRotation;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Transform _anchor;
    [SerializeField] protected Transform _lookAt;

    protected int _multiplier = 1;

    public int DefaulMultiplier { get; protected set; } = 1;
    public Transform LookAt => _lookAt;
    public Rigidbody Rigidbody => _rigidbody;
    public Transform Anchor => _anchor;

    public void SetMultiplier(int multiplier)
    {
        _multiplier= multiplier;
    }

    public void SetDefaultMultiplier()
    {
        _multiplier = DefaulMultiplier;
    }

    public virtual void MoveToTarget(Rigidbody target)
    {
        Vector3 direction = target.position - transform.position;
        _rigidbody.MovePosition(_rigidbody.position + _multiplier * _speedMovement * Time.deltaTime * direction);
    }

    public virtual void RotateToTarget(Transform target)
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, target.position- transform.position);
        transform.rotation = rotation * transform.rotation;
    }

    protected virtual void Move()
    {
    }

    protected virtual void Rotate()
    {
    }

}