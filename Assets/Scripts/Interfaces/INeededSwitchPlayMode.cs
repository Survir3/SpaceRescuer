using UnityEngine.Events;

public interface INeededSwitchPlayMode
{
    public bool IsPause { get; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    public void RequestPlay();
    public void RequestPause();
}
