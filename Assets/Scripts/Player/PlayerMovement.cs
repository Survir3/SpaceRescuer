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
       // _playerInput.Player.Move.performed += ctx => Rotate();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        //_playerInput.Player.Move.performed -= ctx => Rotate();
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

    private Vector3 GetTargetPosition(Vector2 direction)
    {
        return transform.TransformDirection(new Vector3(+direction.x, 0, direction.y));
    }

    private void Rotate()
    {
        float coeff=_playerInput.Player.Move.ReadValue<float>();

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, coeff * 45, 0));

        _rigidbody.MoveRotation(_rigidbody.rotation* targetRotation);
    }
}