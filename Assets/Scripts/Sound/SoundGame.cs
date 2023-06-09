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

        SetSound(PlayerPrefs.GetInt(ConstantsString.OrderSoundPlay));
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
        foreach (var sources in _audioSources)
        {
            sources.mute = true;
        }

        _isPlaying = false;
    }

    private void SetSound(int saveSoundMode)
    {
        int trueValue = 1;

        if(saveSoundMode==trueValue)
        {
            OnSound();
        }
        else
        {
            OffSound();
        }
    }
}
