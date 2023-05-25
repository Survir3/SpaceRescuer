using TMPro;
using UnityEngine;

public class TimerToEndLevel : Timer, IIncreaseForLevel
{
    [SerializeField] private TMP_Text _maxTime;
    [SerializeField] private TMP_Text _currentTime;
    [SerializeField] private TimerStartLevel _timerStartLevel;
    [SerializeField] private Player _player;

    private void Start()
    {
        _currentTime.text =ConvertToClock(_value);
        _maxTime.text ="/ " + ConvertToClock(_value);
    }

    private void OnEnable()
    {
        _timerStartLevel.StartGame += StartTimer;
    }

    private void OnDisable()
    {
        _timerStartLevel.StartGame -= StartTimer;
    }

    public void SetValueToStartLevel(float value)
    {
        _value= value;
    }

    private void ShowValue(float value)
    {
        _currentTime.text = ConvertToClock(value);
    }

    private string ConvertToClock(float value)
    {
        int minutes = (int)(value / 60);
        int seconds = (int)(value % 60);

        return minutes + ":" + seconds;
    }

    private void StartTimer()
    {
        StartCoroutine(Countdown(ShowValue,null, _player.Dead));
    }
}
