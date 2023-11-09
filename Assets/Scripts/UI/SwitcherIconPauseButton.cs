using UnityEngine;
using UnityEngine.UI;

public class SwitcherIconPauseButton : MonoBehaviour
{
    [SerializeField] private HandlerTimeSceler _handlerTimeSceler;
    [SerializeField] private Image _pause;
    [SerializeField] private Image _play;

    private void Awake()
    {
        _play.enabled = _handlerTimeSceler.IsPause;
        _pause.enabled = !_play.enabled;
    }

    public void EnableBackgroundButton()
    {
        if (_handlerTimeSceler.IsGlobalPause && _handlerTimeSceler.IsPause == false)
        {
            return;
        }

        _play.enabled = _handlerTimeSceler.IsPause;
        _pause.enabled = !_play.enabled;
    }
}
