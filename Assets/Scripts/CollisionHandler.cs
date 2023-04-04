using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionHandler : MonoBehaviour
{
    protected IAdder _adderPlayer;
    public bool IsAdded { get; private set; }

    private void Awake()
    {
        _adderPlayer = GetComponent<IAdder>();
    }

    public void AddInSnake()
    {
        IsAdded = true;

        CallAdd();
    }

    protected abstract void CallAdd();
}
