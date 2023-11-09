using UnityEngine.Events;

public interface INeededSwitchPlayMode
{
    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    public bool IsPause { get; }

    public void RequestPlay();
    public void RequestPause();
}
