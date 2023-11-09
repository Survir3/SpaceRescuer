using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class InterstitiaterAd : MonoBehaviour, INeededSwitchPlayMode, INeededSwitchSoundPlay
{
    [SerializeField] private SaverData _saverData;

    private int _multipleСountLoadSceneForAd=5;

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;
    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    public bool IsPause { get; private set; }
    public bool IsOffSound { get; private set; }

    private void Start()
    {
        if(_saverData.СountLoadGame!=0 && _saverData.СountLoadGame%_multipleСountLoadSceneForAd==0)
            InterstitialAd.Show(onOpenCallback, onCloseCallback, null, onCloseCallback);
    }

    private void onOpenCallback()
    {
        RequestPause();
        RequestOffSound();
    }

    private void onCloseCallback()
    {
        RequestPlay();
        RequestOnSound();
    }

    private void onCloseCallback(bool e)
    {
        RequestPlay();
        RequestOnSound();
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
