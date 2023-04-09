using UnityEngine;

[RequireComponent (typeof (Points))]
public class Player : MonoBehaviour
{
    private Points _points;

    public Points Points => _points;

    private void Awake()
    {
        _points= GetComponent<Points>();
    }

    public void Dead()
    {
        Debug.Log("dead");
    }
}