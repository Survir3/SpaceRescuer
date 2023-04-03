using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;

    protected IAdder _adderPlayer;
    protected Collider _collider;
    public bool IsAdded { get; private set; }

    private void Awake()
    {
        _adderPlayer = GetComponent<IAdder>();
        _collider = GetComponent<Collider>();
    }

    public void AddInSnake()
    {
        IsAdded = true;
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
    }
}
