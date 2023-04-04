using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Movement : MonoBehaviour, IMultiplied
{
    [SerializeField] protected float _speedMovement;
    [SerializeField] protected float _speedRotation;
    [SerializeField] protected Rigidbody _rigidbody;

    protected int _multiplier = 1;

    public Transform _anchor;

    public void SetMultiplier(int multiplier)
    {
        _multiplier= multiplier;
    }
}