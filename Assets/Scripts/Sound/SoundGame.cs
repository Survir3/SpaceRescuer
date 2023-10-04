using Agava.WebUtility;
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

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnBackgroundChangeEvent;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChangeEvent;
    }
    private void Start()
    {
        _audioSources = FindObjectsOfType<AudioSource>(true).ToList();
    }

    private void OnBackgroundChangeEvent(bool hidden)
    {
        if (_isPlaying==false)
            return;

        if (hidden)
            OffSound();
        else
            OnSound();
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

        _isPlaying = !_isPlaying;

        ChangedModePlay?.Invoke(_isPlaying);
    }

    public void OnSound()
    {
        foreach (var sources in _audioSources)
        {
            sources.mute = false;
        }
    }

    public void OffSound()
    {
        foreach (var sources in _audioSources)
        {
            sources.mute = true;
        }
    }
}
