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
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void Start()
    {
        SetStartSoundMode(_saverData.OffSound);
    }

    public void OnClickButtonSound()
    {
        if (_isGlobalOffSound)
        {
            RequestOnSound();
        }
        else
        {
            RequestOffSound();
        }
    }

    public void OnSound()
    {
        _isGlobalOffSound= false;

        AudioListener.volume = 1f;
        ChangedModePlay.Invoke(_isGlobalOffSound);
    }

    public void OffSound()
    {
        _isGlobalOffSound = true;

        AudioListener.volume = 0f;
        ChangedModePlay.Invoke(_isGlobalOffSound);
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

    private void AddListeners()
    {
        foreach (var sound in _interfaceSwitchSoundPlay)
        {
            sound.NeededOffSound += OffSound;
            sound.NeededOnSound += TryOnSound;
        }
    }

    private void RemoveListeners()
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

    private void SetStartSoundMode(bool isOffSound)
    {
        if (isOffSound)
        {
            RequestOffSound();
        }
        else
        {
            RequestOnSound();
            TryOnSound();
        }
    }
}
