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
        Action OpenCallback= DisableInputSystem;
        OpenCallback += RequestOffSound;
        OpenCallback += RequestPause;

        Action closeCallbacksAll = RequestPlay;
        closeCallbacksAll += EnableInputSystem;
        closeCallbacksAll += RequestOnSound;

        if (onCloseCallback != null)
        {
            closeCallbacksAll += onCloseCallback;
        }

        VideoAd.Show(OpenCallback, onRewardedCallback, closeCallbacksAll, onErrorCallback);
    }

    private void EnableInputSystem()
    {
        _inputSystem.enabled = true;
    }

    private void DisableInputSystem()
    {
        _inputSystem.enabled = false;
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
        NeededOffSound.Invoke();
    }

    public void RequestOnSound()
    {
        IsOffSound = false;
        NeededOnSound.Invoke();
    }
}
