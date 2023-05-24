using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private bool _isPlaying = true;

    public void OnClickSoundButton()
    {
        if(_isPlaying)
            _audioSource.Pause();
        else
            _audioSource.Play();

        _isPlaying = !_isPlaying;
    }
}
