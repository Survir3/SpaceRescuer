using UnityEngine.Events;

public interface INeededSwitchSoundPlay
{
    public bool IsOffSound { get; }

    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    public void RequestOffSound();
    public void RequestOnSound();
}