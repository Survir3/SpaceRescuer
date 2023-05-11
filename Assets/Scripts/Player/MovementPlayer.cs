using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player), typeof(ControllerSurvivorMovement))]
public class MovementPlayer : Movement
{
    [SerializeField, Range(0, 1)] private float _durationDisableInput;
    [SerializeField] private DetecterOS _detecterOS;

    private PlayerInput _playerInput;
    private ControllerSurvivorMovement _controllerSurvivorMovement;
    private Coroutine _currentCoretine;
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
        if (_detecterOS.Device == RuntimePlatform.Android || _detecterOS.Device == RuntimePlatform.IPhonePlayer)
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

      //  StartDisableInput();
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

        if (_detecterOS.Device ==RuntimePlatform.Android || _detecterOS.Device == RuntimePlatform.IPhonePlayer)
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

    private void StartDisableInput()
    {
        if (_currentCoretine != null)
        {
            StopCoroutine(_currentCoretine);
        }

        _currentCoretine = StartCoroutine(DisableInput());
    }

    private void asdf()
    {
        Debug.Log("asdf");

        _directionRotation = 0;
    }

    private IEnumerator DisableInput()
    {
        _playerInput.Disable();

        yield return new WaitForSeconds(_durationDisableInput);

        _playerInput.Enable();
    }
}