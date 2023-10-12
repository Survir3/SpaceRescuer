using UnityEngine;
using UnityEngine.UI;

public class SwitcherIconSoundButton : MonoBehaviour
{
    [SerializeField] private HandlerSound _handlerSound;
    [SerializeField] private Image _offSoundIcon;

    private void OnEnable()
    {
        _handlerSound.ChangedModePlay += OnChangedIcon;
    }

    private void OnDisable()
    {
        _handlerSound.ChangedModePlay += OnChangedIcon;
    }

    public void OnChangedIcon(bool offSound)
    {
        _offSoundIcon.enabled = offSound;
    }
}
