using UnityEngine;
using UnityEngine.Events;

public class ButtonMenu : MonoBehaviour, INeededSwitchPlayMode
{
    public bool IsPause { get; private set; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    public void OnClickPauseButton()
    {
        if (IsPause)
        {
            RequestPlay();
        }
        else
        {
            RequestPause();
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
}
