using UnityEngine;
using Agava.WebUtility;


public class ViewManagement : MonoBehaviour
{
    [SerializeField] private GameObject _panelMobile;
    [SerializeField] private GameObject _panelComputer;

    private void Awake()
    {
        _panelMobile.SetActive(false);
        _panelComputer.SetActive(false);
    }

    private void Start()
    {
        if (Device.IsMobile)
        {
            _panelMobile.SetActive(true);
        }
        else
        {
            _panelComputer.SetActive(true);
        }
    }
}
