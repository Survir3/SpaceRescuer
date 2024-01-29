using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerSurvivor : Spawner, ISceneLoadHandler<LevelConfig>, INeededSwitchPlayMode
{
    [SerializeField] private Timer _timer;

    public bool IsPause { get; private set; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    private void Start()
    {
        int multiply = 3;
        int countStartSpawner = Count / multiply;

        StartCoroutine(_timer.Countdown(SpawnedToStart, countStartSpawner));
    }

    private void OnEnable()
    {
        IsAllAdded += RequestPause;
    }

    private void OnDisable()
    {
        IsAllAdded -= RequestPause;
    }

    private void Update()
    {
        SpawnedToDelay();
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        _count = argument.CountSurvivorsToLevel;
    }

    public void RequestPlay()
    {
        IsPause = false;
        NeededPlay.Invoke();
    }

    public void RequestPause()
    {
        IsPause = true;
        NeededPause.Invoke();
    }
}
