using UnityEngine;

public class DetecterDevice : MonoBehaviour
{
    public RuntimePlatform Device { get; private set; }

    private void Awake()
    {
        Device=Application.platform;
    }
}
