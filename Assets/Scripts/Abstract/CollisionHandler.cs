using UnityEngine;
using UnityEngine.Events;

public abstract class CollisionHandler : MonoBehaviour
{
    public event UnityAction<CollisionHandler> Added;

    public bool IsAdded { get; protected set; }

    public void AddInSnake()
    {
        IsAdded = true;
        Added?.Invoke(this);
    }
}
