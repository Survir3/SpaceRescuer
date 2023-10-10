using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;

public class HandlerRewardAd : MonoBehaviour, INeededSwitchPlayMode
{
    [SerializeField] private InputSystemUIInputModule _inputSystem;

    public bool IsPause { get; private set; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

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

    public void ShowAd(Action onRewardedCallback = null, Action onCloseCallback = null, Action<string> onErrorCallback = null)
    {
        Action OpenCallback = RequestPause;
        OpenCallback += DisableInputSystem;

        Action closeCallbacksAll = RequestPlay;
        closeCallbacksAll += EnableInputSystem;
        if (onCloseCallback != null)
        {
            closeCallbacksAll += onCloseCallback;
        }

        VideoAd.Show(OpenCallback, onRewardedCallback, closeCallbacksAll, onErrorCallback);
    }
}
