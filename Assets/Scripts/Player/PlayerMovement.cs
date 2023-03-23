using UnityEngine;

[RequireComponent (typeof(Player), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _speedRotation;
    [SerializeField] private Transform _test;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody= GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += ctx => OnRotate();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= ctx => OnRotate();
    }

    private void FixedUpdate()
    {
        Move();
        OnRotate();
    }

    private void Move()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        _rigidbody.MovePosition(_rigidbody.position+ direction * _speedMovement*Time.deltaTime);
    }

    private void OnRotate()
    {
        Quaternion targetRotation = TryGetTargetRotation(_playerInput.Player.Move.ReadValue<float>());
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, _speedRotation * Time.deltaTime);
    }

    private Quaternion TryGetTargetRotation(float direction)
    {
        Vector3 targetPosition=(transform.position + transform.TransformDirection( new Vector3(direction, 0, 0)) - transform.position).normalized;
        return Quaternion.FromToRotation(transform.forward, targetPosition) * _rigidbody.rotation;     
    }
}