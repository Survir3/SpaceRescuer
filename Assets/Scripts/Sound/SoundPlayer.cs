using UnityEngine;
using UnityEngine.Events;

public class SoundPlayer : MonoBehaviour, INeededSwitchSoundPlay
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Player _player;

    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    public bool IsOffSound { get; set; }

    private void OnEnable()
    {
        _player.IsDie += OnDeadPlayer;
    }

    private void OnDisable()
    {
        _player.IsDie -= OnDeadPlayer;
    }

    private void OnDeadPlayer()
    {
        if (IsOffSound == false)
            _audioSource.Play();
    }

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
