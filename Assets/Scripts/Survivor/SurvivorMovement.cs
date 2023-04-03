using UnityEngine;

public class SurvivorMovement : Movement
{
    public void Move(Transform target)
    {
        _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, target.position, _speedMovement * Time.deltaTime);
    }

    public void Rotate(Transform target)
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, target.position- transform.position);
        transform.rotation = rotation* transform.rotation;
    }
}
