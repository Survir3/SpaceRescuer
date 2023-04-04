using UnityEngine;

public class SurvivorMovement : Movement
{
    public void Move(Transform target)
    {
        _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, target.position, _speedMovement* _multiplier * Time.deltaTime);
    }

    public void Rotate(Transform target)
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, target.position- transform.position);
        transform.rotation = rotation* transform.rotation;
    }

    public void SetPositionInSnake(Transform target)
    {
        gameObject.SetActive(false);
        transform.position= target.position;
        gameObject.SetActive(true);
    }
}
