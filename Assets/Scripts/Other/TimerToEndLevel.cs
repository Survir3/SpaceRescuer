using IJunior.TypedScenes;
using TMPro;
using UnityEngine;

public class TimerToEndLevel : Timer, IIncreaseForLevel, ISceneLoadHandler<LevelConfig>
{
    [SerializeField] private TMP_Text _time;
    [SerializeField] private TimerStartLevel _timerStartLevel;
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;

    private Coroutine _lastCoroutine;

    private void Start()
    {
        _startValue = _value;
        ShowValue(_value);
    }

    private void OnEnable()
    {
        _timerStartLevel.StartGame += StartTimer;
        _spawnerSurvivor.IsAllAdded += StopTimer;
        _player.IsDie += StopTimer;
    }

    private void OnDisable()
    {
        _timerStartLevel.StartGame -= StartTimer;
        _spawnerSurvivor.IsAllAdded -= StopTimer;
        _player.IsDie += StopTimer;
    }

    public void SetValue(float value)
    {
        _value= value;
    }

    public void AddValue(float value)
    {
        _value+= value;
    }

    private void ShowValue(float value)
    {
        _time.text = ConvertToClock(_value);
    }

    private string ConvertToClock(float value)
    {
        int minutes = (int)(value / 60);
        int seconds = (int)(value % 60);

        return $"{minutes} : {seconds:D2}";
    }

    private void StartTimer()
    {
        _lastCoroutine = StartCoroutine(Countdown(ShowValue, null, _player.Dead));
    }

    private void StopTimer()
    {
        StopCoroutine(_lastCoroutine);
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        _value = argument.TotalTimeToLevel;
    }
}
