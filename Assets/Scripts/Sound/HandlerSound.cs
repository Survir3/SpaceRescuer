using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HandlerSound : MonoBehaviour, INeededSwitchSoundPlay
{
    [SerializeField] private SaverData _saverData;
    [SerializeField] private MonoBehaviour[] _monoBehaviourSwitchSoundPlay;

    private List<INeededSwitchSoundPlay> _interfaceSwitchSoundPlay;
    private List<AudioSource> _audioSources;
    private bool _isGlobalOffSound = false;

    public bool IsGlobalOffSound => _isGlobalOffSound;

    public bool IsOffSound { get ; private set; }

    public event Action<bool> ChangedModePlay;
    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    private void OnValidate()
    {
        for (int i = 0; i < _monoBehaviourSwitchSoundPlay.Length; i++)
        {
            if (_monoBehaviourSwitchSoundPlay[i] && !(_monoBehaviourSwitchSoundPlay[i] is INeededSwitchSoundPlay))
            {
                Debug.LogError(nameof(_monoBehaviourSwitchSoundPlay) + " needs to implement " + nameof(INeededSwitchSoundPlay));
                _monoBehaviourSwitchSoundPlay[i] = null;
            }
        }
    }

    private void Awake()
    {
        _interfaceSwitchSoundPlay = Array.ConvertAll(_monoBehaviourSwitchSoundPlay, i => (INeededSwitchSoundPlay)i).ToList();
        Debug.Log("_interfaceSwitchSoundPlay" + _interfaceSwitchSoundPlay.Count);
    }

    private void OnEnable()
    {
        AddListener();
    }

    private void OnDisable()
    {
        RemoveListener();
    }

    private void Start()
    {
        _audioSources = FindObjectsOfType<AudioSource>(true).ToList();

        _isGlobalOffSound = _saverData.IsOffSound();

        if (_isGlobalOffSound)
        {
            OffSound();
        }
        else
        {
            OnSound();
        }
    }

    public void OnClickButtonSound()
    {
        if (_isGlobalOffSound)
        {
            RequestOnSound();
            TryOnSound();
        }
        else
            RequestOffSound();
    }

    private void AddListener()
    {
        foreach (var sound in _interfaceSwitchSoundPlay)
        {
            Debug.Log(sound);
            sound.NeededOffSound += OffSound;
            sound.NeededOnSound += TryOnSound;
        }
    }

    private void RemoveListener()
    {
        foreach (var sound in _interfaceSwitchSoundPlay)
        {
            sound.NeededOffSound -= OffSound;
            sound.NeededOnSound -= TryOnSound;
        }
    }

    private void TryOnSound()
    {
        bool isAllTriggerReadyPlay = true;

        foreach (var sound in _interfaceSwitchSoundPlay)
        {


            if (sound.IsOffSound)
            {
                isAllTriggerReadyPlay = false;
                return;
            }
        }

        if (isAllTriggerReadyPlay)
        {
            OnSound();
        }
    }

    public void OnSound()
    {
        _isGlobalOffSound= false;

        foreach (var sources in _audioSources)
        {
            sources.mute = _isGlobalOffSound;
        }

        ChangedModePlay.Invoke(_isGlobalOffSound);
    }

    public void OffSound()
    {
        _isGlobalOffSound = true;

        foreach (var sources in _audioSources)
        {
            sources.mute = _isGlobalOffSound;
        }

        ChangedModePlay.Invoke(_isGlobalOffSound);
    }

    public void RequestOffSound()
    {
        IsOffSound = true;
        Debug.Log("HandlerSound " + IsOffSound);

        foreach (var interfaceSwitchSoundPlay in _interfaceSwitchSoundPlay)
        {
            Debug.Log("foreach " + interfaceSwitchSoundPlay.IsOffSound);
        }

        NeededOffSound.Invoke();
    }

    public void RequestOnSound()
    {
        IsOffSound = false;
        Debug.Log("HandlerSound " + IsOffSound);

        foreach (var interfaceSwitchSoundPlay in _interfaceSwitchSoundPlay)
        {
            Debug.Log("foreach " + interfaceSwitchSoundPlay.IsOffSound);
        }

        NeededOnSound.Invoke();
    }
}
