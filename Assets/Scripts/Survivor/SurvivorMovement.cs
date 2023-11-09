using UnityEngine;

public class SurvivorMovement : Movement
{
    public void SetStart(Vector3 startPosition, int multiplierMove)
    {
        _currentMultiplier = multiplierMove;
        _rigidbody.position = startPosition;
        _rigidbody.velocity = Vector3.zero;
    }

    public override void Move(Vector3 target)
    {
          Vector3 direction = target - transform.position;
          _rigidbody.MovePosition(_rigidbody.position + direction * SpeedMovement * _currentMultiplier * Time.fixedDeltaTime);
    }
}
