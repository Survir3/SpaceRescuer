using UnityEngine;
using TMPro;
using System;

public class TimerStartLevel : Timer
{
    [SerializeField] private TMP_Text _scoreboard;
    [SerializeField] private MovementPlayer _movementPlayer;
    [SerializeField] private string _startText;

    public event Action StartGame; 

    private void Start()
    {
        StartCoroutine(Countdown(ShowValue, CreateStartTimer, CreateEndTimer));
    }

    private void ShowValue(float value)
    {
        _scoreboard.text = ((int)value).ToString();
    }

    private void CreateStartTimer()
    {
        _movementPlayer.enabled = false;
    }

    private void CreateEndTimer()
    {
        _scoreboard.enabled = false;
        _movementPlayer.enabled = true;
        StartGame?.Invoke();
    }
}
