using UnityEngine;

public abstract class Movement : MonoBehaviour, IMultiplied
{
    [SerializeField] protected float SpeedMovement;
    [SerializeField] protected float SpeedRotation;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Transform _anchor;
    [SerializeField] protected Transform _lookAt;

    public int _currentMultiplier = 1;

    public int DefaulMultiplier { get; protected set; } = 1;
    public int CurrentMultiplier =>_currentMultiplier;
    public Transform LookAt => _lookAt;
    public Rigidbody Rigidbody => _rigidbody;
    public Transform Anchor => _anchor;

    public void SetMultiplier(int multiplier)
    {
        _currentMultiplier= multiplier;
    }

    public void SetDefaultMultiplier()
    {
        _currentMultiplier = DefaulMultiplier;
    }

    public virtual void Move(Vector3 point)
    {
    }

    protected virtual void Move()
    {
    }

    protected virtual void Rotate()
    {
    }
}