using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;

public class HandlerRewardAd : MonoBehaviour, INeededSwitchPlayMode, INeededSwitchSoundPlay
{
    [SerializeField] private InputSystemUIInputModule _inputSystem;

    public bool IsPause { get; private set; }

    public bool IsOffSound { get; private set; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;
    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    public void ShowAd(Action onRewardedCallback = null, Action onCloseCallback = null, Action<string> onErrorCallback = null)
    {
        Action OpenCallback= DisableInputSystem ;
        OpenCallback += RequestOffSound;
        OpenCallback += RequestPause;

        Action closeCallbacksAll = RequestPlay;
        closeCallbacksAll += EnableInputSystem;
        closeCallbacksAll += RequestOnSound;

        if (onCloseCallback != null)
        {
            closeCallbacksAll += onCloseCallback;
        }

        VideoAd.Show(OpenCallback, onRewardedCallback, closeCallbacksAll);
    }

    private void EnableInputSystem()
    {
        Debug.Log("EnableInputSystem");

        _inputSystem.enabled = true;
    }

    private void DisableInputSystem()
    {
        Debug.Log("DisableInputSystem");

        _inputSystem.enabled = false;
    }

    public void RequestPlay()
    {
        Debug.Log("RequestPlay");

        IsPause = false;
        NeededPlay.Invoke();
    }

    public void RequestPause()
    {
        Debug.Log("RequestPause");

        IsPause = true;
        NeededPause.Invoke();
    }

    public void RequestOffSound()
    {
        IsOffSound = true;
        Debug.Log("HandlerRewardAd " + IsOffSound);
        NeededOffSound.Invoke();
    }

    public void RequestOnSound()
    {

        IsOffSound = false;
        Debug.Log("HandlerRewardAd " + IsOffSound);
        NeededOnSound.Invoke();
    }
}
