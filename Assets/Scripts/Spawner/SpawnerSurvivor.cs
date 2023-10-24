using IJunior.TypedScenes;
using UnityEngine.Events;

public class SpawnerSurvivor : Spawner, ISceneLoadHandler<LevelConfig>, INeededSwitchPlayMode
{
    public bool IsPause { get; private set; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

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
        Spawned();
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
