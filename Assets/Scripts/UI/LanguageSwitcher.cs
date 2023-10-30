using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private void Start()
    {
        LoadLocalization();
    }

    private void LoadLocalization()
    {
        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "ru":
                _leanLocalization.SetCurrentLanguage("Russian");
                break;
            case "tr":
                _leanLocalization.SetCurrentLanguage("Turkish");
                break;
            case "en":
                _leanLocalization.SetCurrentLanguage("English");
                break;
            default:
                _leanLocalization.SetCurrentLanguage("Russian");
                break;
        }
    }
}
