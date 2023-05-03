using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Points))]
public class Player : MonoBehaviour
{
    private Points _points;

    public Points Points => _points;

    public event UnityAction IsDead;

    private void Awake()
    {
        _points= GetComponent<Points>();
    }

    public void Dead()
    {
        IsDead.Invoke();
    }
}