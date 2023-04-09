using UnityEngine;
using UnityEngine.Events;

public abstract class CollisionHandler : MonoBehaviour
{
    protected IAdder _adderPlayer;
    public bool IsAdded { get; private set; }
    public IAdder AdderPlayer => _adderPlayer;

    public event UnityAction Added;

    private void Awake()
    {
        _adderPlayer = GetComponent<IAdder>();
    }

    public void AddInSnake()
    {
        IsAdded = true;
        Added?.Invoke();
    }
}
