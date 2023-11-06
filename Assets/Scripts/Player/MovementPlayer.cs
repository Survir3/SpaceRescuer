using Agava.WebUtility;
using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(HandlerPathSnake))]
public class MovementPlayer : Movement, IIncreaseForLevel, ISceneLoadHandler<LevelConfig>
{
    [SerializeField, Range(0, 1)] private float _durationDisableInput;

    private delegate float ActionInput(float input);
    private PlayerInput _playerInput;
    private HandlerPathSnake _handlerSurvivorMovements;
    private float _directionRotation;
    private Quaternion _targetRotation;
    private ActionInput _onCorrectInputForDevice;

    public float SpeedMovenemt => _speedMovement;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _targetRotation = _rigidbody.rotation;
        _handlerSurvivorMovements = GetComponent<HandlerPathSnake>();
    }

    private void Start()
    {
#if UNITY_EDITOR
            _onCorrectInputForDevice = InputKeyboard;
            return;
#endif

        if (Device.IsMobile)
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
        newList.AddRange(_handlerSurvivorMovements.MovementsSnake);

        return newList;
    }

    protected override void Move()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward).normalized;
        _rigidbody.MovePosition(_rigidbody.position + direction * _speedMovement * _currentMultiplier * Time.fixedDeltaTime);
    }

    protected override void Rotate()
    {
        _targetRotation=GetTargetRotation();
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, _targetRotation, _speedRotation * Time.fixedDeltaTime);
    }

    private Quaternion GetTargetRotation()
    {
        _directionRotation = _onCorrectInputForDevice(_playerInput.Player.Move.ReadValue<float>());

        Vector3 targetPosition = transform.TransformDirection(new Vector3(_directionRotation, 0, 1));
        return Quaternion.FromToRotation(transform.forward, targetPosition) * _rigidbody.rotation;
    }

    private float InputForTouchDevice(float value)
    {
        int countTouches = 1;

        if (_playerInput.Player.Touch.ReadValue<float>() != countTouches)
            return 0;

        float left = -1;
        float right = 1;
        float centrScreen = Screen.width / 2;

        if (value < centrScreen)
            return left;
        else
            return right;
    }

    private float InputKeyboard(float value)
    {
        return value;
    }

    public void SetValue(float value)
    {
        _speedMovement = value;
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        SetValue(argument.SpeedMovement);
    }
}