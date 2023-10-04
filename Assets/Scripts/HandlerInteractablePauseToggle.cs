using UnityEngine;
using UnityEngine.UI;

public class HandlerInteractablePauseToggle : MonoBehaviour
{
    [SerializeField] private Toggle _pauseToggle;
    [SerializeField] private Training _training;

    private void OnEnable()
    {
        _training.IsTraining += OffButton;
    }

    private void OnDisable()
    {
        _training.IsTraining -= OffButton;
    }

    private void OffButton(string arg0)
    {
        _pauseToggle.interactable = false;
    }

    public void OnButton()
    {
        _pauseToggle.interactable = true;
    }
}
