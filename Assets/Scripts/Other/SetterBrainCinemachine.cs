using Agava.WebUtility;
using Cinemachine;
using UnityEngine;

public class SetterBrainCinemachine : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mobile;
    [SerializeField] private CinemachineVirtualCamera _computer;

    private int _activePriority = 1;

    private void Awake()
    {
        _mobile.Priority = 0;
        _computer.Priority= 0;
    }

    private void Start()
    {
        if (Device.IsMobile)
            _mobile.Priority = _activePriority;
        else
            _computer.Priority = _activePriority;
    }
}
