using IJunior.TypedScenes;
using TMPro;
using UnityEngine;

public class TimerToEndLevel : Timer, IIncreaseForLevel, ISceneLoadHandler<DataLoadScene>
{
    [SerializeField] private TMP_Text _maxTime;
    [SerializeField] private TMP_Text _currentTime;
    [SerializeField] private TimerStartLevel _timerStartLevel;
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;

    private Coroutine _lastCoroutine;

    private void Start()
    {
        _currentTime.text =ConvertToClock(_value);
        _maxTime.text ="/ " + ConvertToClock(_value);
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
        _lastCoroutine = StartCoroutine(Countdown(ShowValue, null, _player.Dead));
    }

    private void StopTimer()
    {
        StopCoroutine(_lastCoroutine);
    }

    public void OnSceneLoaded(DataLoadScene argument)
    {
        _value = argument.LevelConfig.TotalTimeToLevel;
    }
}
