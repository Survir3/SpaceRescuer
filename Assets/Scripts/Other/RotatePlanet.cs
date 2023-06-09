using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    [SerializeField] private int _speed;

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime* _speed);
    }
}
