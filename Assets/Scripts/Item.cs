using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour, IGiverPoints, IAdder
{
    [SerializeField] private int _points;

    public int Points => _points;

    public event UnityAction Added;

    public void Add()
    {
        Added?.Invoke();
    }
}
