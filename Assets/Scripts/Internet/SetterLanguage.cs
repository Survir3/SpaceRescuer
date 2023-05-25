using UnityEngine;
using Lean.Localization;
using Agava.YandexGames;

public class SetterLanguage : MonoBehaviour
{
    private void Start()
    {
       SetLanguage(YandexGamesSdk.Environment.i18n.lang);
    }

    private void SetLanguage(string lang)
    {
        string language;

        switch (lang)
        {
            case Language.LangRussian: 
                language = Language.Russian; 
                break;
            case Language.LangTurkish: 
                language = Language.Turkish; 
                break;
            default:
                language = Language.English; 
                break;
        }

        LeanLocalization.SetCurrentLanguageAll(language);
    }
}
