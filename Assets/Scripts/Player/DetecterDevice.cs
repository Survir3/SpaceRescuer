using Agava.WebUtility;
using UnityEngine;

public class DetecterDevice : MonoBehaviour
{
    public bool IsDevice { get; private set; }

    private void Awake()
    {
        IsDevice=Device.IsMobile;
    }
}
