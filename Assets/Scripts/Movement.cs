using UnityEngine;

public abstract class Movement : MonoBehaviour, IMultiplied
{
    [SerializeField] protected float _speedMovement;
    [SerializeField] protected float _speedRotation;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] private  Transform _anchor;

    protected int _multiplier = 1;

    public int DefaulMultiplier { get; protected set; } = 1;
    public Transform Anchor=> _anchor;
    public void SetMultiplier(int multiplier)
    {
        _multiplier= multiplier;
    }

    public void SetDefaultMultiplier()
    {
        _multiplier = DefaulMultiplier;
    }
}