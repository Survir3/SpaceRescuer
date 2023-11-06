using IJunior.TypedScenes;
using UnityEngine.Events;

public class SpawnerSurvivor : Spawner, ISceneLoadHandler<LevelConfig>, INeededSwitchPlayMode
{
    public bool IsPause { get; private set; }

    private int _countStartSpawner => _count / 2;

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    private void Start()
    {
        SpawnedToStart(_countStartSpawner);
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
