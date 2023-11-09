using UnityEngine.Events;

public interface INeededSwitchSoundPlay
{
    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    public bool IsOffSound { get; }

    public void RequestOffSound();
    public void RequestOnSound();
}