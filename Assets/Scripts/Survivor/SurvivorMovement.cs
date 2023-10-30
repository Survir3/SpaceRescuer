using UnityEngine;

public class SurvivorMovement : Movement
{
    public void SetStart(Transform target, int multiplierMove)
    {
        _currentMultiplier=multiplierMove;
        _rigidbody.position= target.position;
        _rigidbody.velocity = Vector3.zero;
    }

    public override void MoveToTarget(Rigidbody target)
    {
         Vector3 direction =  target.position- transform.position;
         _rigidbody.MovePosition(_rigidbody.position + _currentMultiplier * _speedMovement * Time.fixedDeltaTime * direction);
    }
}
