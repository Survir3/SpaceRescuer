using Agava.WebUtility;
using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.Events;

public class HandlerRewardAd : MonoBehaviour, INeededSwitchPlayMode
{
    public bool IsPause { get; private set; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

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
        Action closeCallbacksAll = RequestPlay;

        if (onCloseCallback != null)
        {
            closeCallbacksAll += onCloseCallback;
        }

        VideoAd.Show(RequestPause, onRewardedCallback, closeCallbacksAll, onErrorCallback);
    }
}
