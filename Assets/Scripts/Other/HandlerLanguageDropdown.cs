using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class HandlerLanguageDropdown : MonoBehaviour
{  
    private Dropdown _dropdown;

    private void Awake()
    {
        _dropdown= GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void OnClickDropdown(int index)
    {
       string lang = _dropdown.options[index].text;
    }

    private void OnChangedLanguage(Sprite flag)
    {
        _dropdown.captionImage.sprite = flag;
        Debug.Log(flag);
    }
}
