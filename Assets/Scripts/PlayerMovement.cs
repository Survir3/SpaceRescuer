using UnityEngine;

[RequireComponent (typeof(Player), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;

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

    private void Update()
    {
        Vector2 direction = _playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 targetPosition = GetTargetPosition(direction);

        Move(targetPosition);
    }

    private void Move(Vector3 targetPosition)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(targetPosition*_speedMovement);
    }

    private Vector3 GetTargetPosition(Vector2 direction)
    {
        return transform.TransformDirection(new Vector3(+direction.x, 0, direction.y));
    }
}