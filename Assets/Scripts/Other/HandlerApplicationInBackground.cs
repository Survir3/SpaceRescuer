using Agava.WebUtility;
using UnityEngine;
using UnityEngine.Events;

public class HandlerApplicationInBackground : MonoBehaviour, INeededSwitchPlayMode
{
    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    public bool IsPause { get; private set; }

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeEvent;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeEvent;
    }

    public void OnInBackgroundChangeEvent(bool hidden)
    {
        AudioListener.pause = hidden;
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
