using UnityEngine;
using UnityEngine.Events;

public class SoundAdded : MonoBehaviour, INeededSwitchSoundPlay
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CollisionHandler _collisionHandler;

    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    public bool IsOffSound { get; set; }

    private void OnEnable()
    {
        _collisionHandler.Added += OnAdded;
    }

    private void OnDisable()
    {
        _collisionHandler.Added -= OnAdded;
    }

    private void OnAdded(CollisionHandler collisionHandler)
    {
        if (IsOffSound == false)
        {
            _audioSource.Play();
        }
    }

    public void RequestOffSound()
    {
        IsOffSound= true;
        NeededOffSound.Invoke();
    }

    public void RequestOnSound()
    {
        IsOffSound = false;
        NeededOnSound.Invoke();
    }
}
