using UnityEngine;
using UnityEngine.UI;

public class SwitcherIconButton : MonoBehaviour
{
    [SerializeField] private Image _background;

    public void EnableBackgroundButton()
    {
        _background.enabled = !_background.enabled;
    }
}
