using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Rigidbody))]
public class MovementPlayer : Movement
{
    [SerializeField, Range(0,1)] private float _durationDisableInput;

    private PlayerInput _playerInput;
    private Coroutine _currentCoretine;
    private float _directionRotation;
    private Quaternion _targetRotation;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _targetRotation = _rigidbody.rotation;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += ctx => GetTargetRotation();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= ctx => GetTargetRotation();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public void Move()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        _rigidbody.MovePosition(_rigidbody.position + direction * _speedMovement * Time.deltaTime);
    }

    private void Rotate()
    {
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, _targetRotation, _speedRotation * Time.deltaTime);
    }

    private void GetTargetRotation()
    {
        _directionRotation = _playerInput.Player.Move.ReadValue<float>();
        Vector3 targetPosition = transform.TransformDirection(new Vector3(_directionRotation, 0, 0));
        _targetRotation = Quaternion.FromToRotation(transform.forward, targetPosition) * _rigidbody.rotation;

        StartDisableInput();
    }

    private void StartDisableInput()
    {
        if(_currentCoretine!=null)
        {
            StopCoroutine(_currentCoretine);
        }

        _currentCoretine=StartCoroutine(DisableInput());
    }

    private IEnumerator DisableInput()
    {
        _playerInput.Disable();
        
        yield return new WaitForSeconds(_durationDisableInput);

        _playerInput.Enable();
    }
}