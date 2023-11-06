using UnityEngine;
using UnityEngine.Events;

public abstract class CollisionHandler : MonoBehaviour
{
    public bool IsAdded { get; protected set; }

    public event UnityAction<CollisionHandler> Added;

    public void AddInSnake()
    {
        IsAdded = true;
        Added?.Invoke(this);
    }
}
