using UnityEngine;
using UnityEngine.Events;

public class SoundGame : MonoBehaviour, INeededSwitchSoundPlay
{
    [SerializeField] private AudioSource _audioSource;

    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    public bool IsOffSound { get; set; }

    public void RequestOffSound()
    {
        IsOffSound = true;
        NeededOffSound.Invoke();  
    }

    public void RequestOnSound()
    {
        IsOffSound = false;
        NeededOnSound.Invoke();
    }
}
