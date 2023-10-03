using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundGame : MonoBehaviour
{
    private List<AudioSource> _audioSources;
    private bool _isPlaying = true;

    public bool IsPlaying => _isPlaying;

    public event Action<bool> ChangedModePlay;

    private void Start()
    {
        _audioSources = FindObjectsOfType<AudioSource>(true).ToList();
    }

    public void OnSwitchSound()
    {
        if (_isPlaying)
        {
            OffSound();
        }
        else
        {
            OnSound();
        }

        ChangedModePlay?.Invoke(_isPlaying);
    }

    public void OnSound()
    {
        foreach (var sources in _audioSources)
        {
            sources.mute = false;
        }

        _isPlaying = true;
    }

    public void OffSound()
    {
        Debug.Log(_isPlaying);

        foreach (var sources in _audioSources)
        {
            sources.mute = true;
        }

        _isPlaying = false;
    }
}
