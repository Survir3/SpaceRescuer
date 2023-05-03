using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
    protected IAdder _adderPlayer;
    public bool IsAdded { get; protected set; }
    public IAdder AdderPlayer => _adderPlayer;

    public event UnityAction<CollisionHandler> Added;

    private void Awake()
    {
        _adderPlayer = GetComponent<IAdder>();
    }

    public void AddInSnake()
    {
        IsAdded = true;
        Added?.Invoke(this);
    }
}
