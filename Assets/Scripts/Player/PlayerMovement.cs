using UnityEngine;

[RequireComponent (typeof(Player), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _speedRotation;

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
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        _rigidbody.MovePosition(_rigidbody.position+ direction * _speedMovement*Time.deltaTime);
    }

    private void Rotate()
    {
        Quaternion targetRotation = GetTargetRotation(_playerInput.Player.Move.ReadValue<float>());
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, _speedRotation * Time.deltaTime);
    }

    private Quaternion GetTargetRotation(float direction)
    {
        Vector3 targetPosition=transform.TransformDirection( new Vector3(direction, 0, 0));
        return Quaternion.FromToRotation(transform.forward, targetPosition) * _rigidbody.rotation;     
    }
}