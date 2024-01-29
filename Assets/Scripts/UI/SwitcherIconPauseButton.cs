using UnityEngine;
using UnityEngine.UI;

public class SwitcherIconPauseButton : MonoBehaviour
{
    [SerializeField] private HandlerTimeSceler _handlerTimeSceler;
    [SerializeField] private Image _pause;
    [SerializeField] private Image _play;

    private void Awake()
    {
        _play.enabled = HandlerTimeSceler.IsGlobalPause;
        _pause.enabled = !_play.enabled;
    }


    private void OnEnable()
    {
        HandlerTimeSceler.ChangedGlobalPause += OnChangedGlobalPause;
    }

    private void OnDisable()
    {
        HandlerTimeSceler.ChangedGlobalPause -= OnChangedGlobalPause;
    }

    private void OnChangedGlobalPause(bool isPause)
    {
        _play.enabled = isPause;
        _pause.enabled = !_play.enabled;
    }
}
