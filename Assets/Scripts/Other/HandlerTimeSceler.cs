using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HandlerTimeSceler : MonoBehaviour, INeededSwitchPlayMode
{
    public static bool IsGlobalPause { get; private set; }

    public static event UnityAction<bool> ChangedGlobalPause;

    [SerializeField] private MonoBehaviour[] _monoBehaviourTriggersGamePause;

    private List<INeededSwitchPlayMode> _interfaceTriggersGamePause;

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    public bool IsPause { get; private set; }

    private void OnValidate()
    {
        for (int i = 0; i < _monoBehaviourTriggersGamePause.Length; i++)
        {
            if (_monoBehaviourTriggersGamePause[i] && !(_monoBehaviourTriggersGamePause[i] is INeededSwitchPlayMode))
            {
                Debug.LogError(nameof(_monoBehaviourTriggersGamePause) + " needs to implement " + nameof(INeededSwitchPlayMode));
                _monoBehaviourTriggersGamePause[i] = null;
            }
        }
    }

    private void Awake()
    {
        SetPlay();
        _interfaceTriggersGamePause = Array.ConvertAll(_monoBehaviourTriggersGamePause, i => (INeededSwitchPlayMode)i).ToList();
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        foreach (var trigger in _interfaceTriggersGamePause)
        {
            trigger.NeededPause += SetPause;
            trigger.NeededPlay += TrySetPlay;
        }
    }

    private void RemoveListeners()
    {
        foreach (var trigger in _interfaceTriggersGamePause)
        {
            trigger.NeededPause -= SetPause;
            trigger.NeededPlay -= TrySetPlay;
        }
    }

    public void OnClickPauseButton()
    {
        if (IsGlobalPause)
        {
            RequestPlay();
        }
        else
        {
            RequestPause();
        }
    }

    private void TrySetPlay()
    {
        bool isAllTriggerReadyPlay = true;

        foreach (var trigger in _interfaceTriggersGamePause)
        {
            if (trigger.IsPause)
            {
                isAllTriggerReadyPlay = false;
                return;
            }
        }

        if (isAllTriggerReadyPlay)
        {
            SetPlay();
        }
    }

    public void RequestPlay()
    {
        IsPause = false;
        NeededPlay.Invoke();
    }

    public void RequestPause()
    {
        IsPause = true;
        NeededPause.Invoke();
    }

    private void SetPause()
    {
        Time.timeScale = 0;
        IsGlobalPause = true;
        ChangedGlobalPause?.Invoke(IsGlobalPause);
    }

    private void SetPlay()
    {
        Time.timeScale = 1;
        IsGlobalPause = false;
        ChangedGlobalPause?.Invoke(IsGlobalPause);
    }
}