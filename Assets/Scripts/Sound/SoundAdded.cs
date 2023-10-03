using UnityEngine;

public class SoundAdded : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CollisionHandler _collisionHandler;

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
        _audioSource.Play();
    }
}
