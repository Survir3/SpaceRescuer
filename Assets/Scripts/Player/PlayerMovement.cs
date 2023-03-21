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
        Move();
    }

    private void Move()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        _rigidbody.AddForce(direction * _speedMovement*Time.deltaTime);
    }

    private Vector3 GetTargetPosition(Vector2 direction)
    {
        return transform.TransformDirection(new Vector3(+direction.x, 0, direction.y));
    }
}