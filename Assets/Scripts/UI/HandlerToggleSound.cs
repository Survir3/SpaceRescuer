using Agava.WebUtility;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HandlerToggleSound : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private SoundGame _soundGame;

    private void Start()
    {
        bool isPlaying=Convert.ToBoolean(PlayerPrefs.GetInt(ConstantsString.OrderSoundPlay));

        if (!isPlaying)
            _toggle.isOn = isPlaying;
    }
}
