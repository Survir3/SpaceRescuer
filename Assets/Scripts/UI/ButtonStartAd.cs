using UnityEngine;
using UnityEngine.UI;

public class ButtonStartAd : MonoBehaviour
{
    [SerializeField] private HandlerRewardAd _handlerRewardAd;
    [SerializeField] private Button _button;

    private void Disable()
    {
        _button.interactable= false;
    }
}
