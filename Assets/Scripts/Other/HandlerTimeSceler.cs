using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HandlerTimeSceler : MonoBehaviour, INeededSwitchPlayMode
{
    [SerializeField] private MonoBehaviour[] _monoBehaviourTriggersGamePause;

    private List<INeededSwitchPlayMode> _interfaceTriggersGamePause;

    private bool _isGlobalPause = false;

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    public bool IsPause { get; private set; }
    public bool IsGlobalPause => _isGlobalPause;

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
        _interfaceTriggersGamePause= Array.ConvertAll(_monoBehaviourTriggersGamePause, i => (INeededSwitchPlayMode)i).ToList();
    }

    private void OnEnable()
    {
        AddListener();
    }

    private void OnDisable()
    {
        RemoveListener();
    }

    private void AddListener()
    {
        foreach (var trigger in _interfaceTriggersGamePause)
        {
            trigger.NeededPause += SetPause;
            trigger.NeededPlay += SetPlay;
        }
    }

    private void RemoveListener()
    {
        foreach (var trigger in _interfaceTriggersGamePause)
        {
            trigger.NeededPause -= SetPause;
            trigger.NeededPlay -= SetPlay;
        }
    }

    public void OnClickPauseButton()
    {
        if (_isGlobalPause)
        {
            RequestPlay();
            SetPlay();
        }
        else
        {
            RequestPause();
        }
    }

    private void SetPlay()
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
            Time.timeScale = 1;
            _isGlobalPause = false;
        }
    }

    private void SetPause()
    {
        Time.timeScale = 0;
        _isGlobalPause = true;
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
}
