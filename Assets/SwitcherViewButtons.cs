using System.Collections.Generic;
using UnityEngine;

public class SwitcherViewButtons : MonoBehaviour
{
    [SerializeField] List<GameObject> _buttons;

    private bool _isActive=false;

    private void Awake()
    {
        SetView();
    }

    public void OnClickButton()
    {
        _isActive = !_isActive;
        SetView();
    }

    private void SetView()
    {
        foreach (var button in _buttons)
        {
            button.SetActive(_isActive);
        }
    }
}
