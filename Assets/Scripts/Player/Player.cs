using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Points))]
public class Player : MonoBehaviour
{
    private Points _points;

    public bool IsDead { get; private set; } = false;
    public Points Points => _points;

    public event UnityAction IsDie;

    private void Awake()
    {
        _points= GetComponent<Points>();
    }

    public void Dead()
    {
        IsDead=true;
        IsDie.Invoke();
    }
}