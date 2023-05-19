using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Points))]
public class Player : MonoBehaviour
{
   [SerializeField] private Points _points;

    public bool IsDead { get; private set; } = false;
    public Points Points => _points;

    public event UnityAction IsDie;

    public void Dead()
    {
        IsDead=true;
        IsDie.Invoke();
    }
}