using Agava.WebUtility;
using UnityEngine;
using UnityEngine.Events;

public class HandlerApplicationInBackground : MonoBehaviour, INeededSwitchPlayMode, INeededSwitchSoundPlay
{
    public bool IsPause { get; private set; }
    public bool IsOffSound { get; private set; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;
    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

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
        if (hidden)
        {
            RequestPause();
            RequestOffSound();
        }
        else
        {
            RequestPlay();
            RequestOnSound();
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

    public void RequestOffSound()
    {

        IsOffSound = true;
        Debug.Log("HandlerApplicationInBackground "+ IsOffSound);
        NeededOffSound.Invoke();
        
    }

    public void RequestOnSound()
    {
        IsOffSound = false;
        Debug.Log("HandlerApplicationInBackground " + IsOffSound);

        NeededOnSound.Invoke(); 
    }
}
