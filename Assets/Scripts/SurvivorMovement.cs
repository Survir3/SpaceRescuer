using UnityEngine;

public class SurvivorMovement : Movement
{
    [SerializeField] Transform _mobel;

    public void Move(Transform target)
    {
        _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, target.position, _speedMovement * Time.deltaTime);
    }

    public void Rotate(Transform target)
    {
        Quaternion rotation = Quaternion.FromToRotation(_mobel.forward, target.position - _mobel.position);
        _mobel.rotation = rotation* _mobel.rotation;
    }
}
