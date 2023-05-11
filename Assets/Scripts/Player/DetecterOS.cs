using UnityEngine;

public class DetecterOS : MonoBehaviour
{
    public RuntimePlatform Device { get; private set; }

    private void Awake()
    {
        Device=Application.platform;
    }
}
