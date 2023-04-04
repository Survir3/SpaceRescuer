using UnityEngine;
using UnityEngine.Events;

[RequireComponent((typeof(IAdder)))]
public class CollisionSurvivorHandler : CollisionHandler
{
    public event UnityAction HasAdd;

    protected override void CallAdd()
    {
        HasAdd?.Invoke();
    }
}
