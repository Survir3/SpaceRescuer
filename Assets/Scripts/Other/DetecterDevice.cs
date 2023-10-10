using Agava.WebUtility;
using UnityEngine;

public class DetecterDevice : MonoBehaviour
{
    public bool IsMobile { get; private set; }

    private void Awake()
    {
        IsMobile=Device.IsMobile;
    }
}
