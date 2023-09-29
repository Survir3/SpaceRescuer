using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitcherIconButton : MonoBehaviour
{
    [SerializeField] private Image _background;

    public void SwitchIconButton()
    {
        _background.enabled = !_background.enabled;
    }
}
