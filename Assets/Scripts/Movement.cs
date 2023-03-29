using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float _speedMovement;
    [SerializeField] protected float _speedRotation;
    [SerializeField] protected Rigidbody _rigidbody;

    public Transform _anchor;
}