using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player), typeof(ControllerSurvivorMovement))]
public class MovementPlayer : Movement, IIncreaseForLevel
{
    [SerializeField, Range(0, 1)] private float _durationDisableInput;
    [SerializeField] private DetecterDevice _detecterDevice;

    private PlayerInput _playerInput;
    private ControllerSurvivorMovement _controllerSurvivorMovement;
    private float _directionRotation;
    private Quaternion _targetRotation;

    private delegate float ActionInput(float input);
    private ActionInput _onCorrectInputForDevice;

    public float SpeedMovenemt => _speedMovement;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _targetRotation = _rigidbody.rotation;
        _controllerSurvivorMovement = GetComponent<ControllerSurvivorMovement>();
    }

    private void Start()
    {
        if (_detecterDevice.Device == RuntimePlatform.Android || _detecterDevice.Device == RuntimePlatform.IPhonePlayer)
            _onCorrectInputForDevice = InputForTouchDevice;
        else
            _onCorrectInputForDevice = InputKeyboard;
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

    public List<IMultiplied> GetAllMovementShake()
    {
        var newList = new List<IMultiplied>();
        newList.Add(this);
        newList.AddRange(_controllerSurvivorMovement.SurvivorMovements);

        return newList;
    }

    protected override void Move()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        _rigidbody.MovePosition(_rigidbody.position + direction * _speedMovement*_currentMultiplier * Time.deltaTime);
    }

    protected override void Rotate()
    {
        GetTargetRotation();
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, _targetRotation, _speedRotation * Time.deltaTime);
    }

    private void GetTargetRotation()
    {        
        _directionRotation = _onCorrectInputForDevice(_playerInput.Player.Move.ReadValue<float>());

        Vector3 targetPosition = transform.TransformDirection(new Vector3(_directionRotation, 0, 1));
        _targetRotation = Quaternion.FromToRotation(transform.forward, targetPosition) * _rigidbody.rotation;
    }

    private float InputForTouchDevice(float value)
    {
        int touchTrue = 1;

        if(_playerInput.Player.Touch.ReadValue<float>()!=touchTrue)
        {
            return 0;
        }

        float left = -1;
        float right = 1;
        float centrScreen = Screen.width / 2;        

        if (_detecterDevice.Device ==RuntimePlatform.Android || _detecterDevice.Device == RuntimePlatform.IPhonePlayer)
        {
            if(value< centrScreen)
            {
                return left;
            }
            else 
            {
                return right;
            }
        }

        return 0;
    }

    private float InputKeyboard(float value)
    {
        return value;
    }

    public void SetValueToStartLevel(float value)
    {
        _speedMovement = value;
    }
}