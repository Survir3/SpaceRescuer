using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ConnecterYandex : MonoBehaviour
{
    public event UnityAction IsConnect;
   // public event UnityAction<string> ReadySetLanguage;
   // public event UnityAction ReadyLoadedLeaderboard;

    private void Awake()
    {        
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.ConectWhisSDKSceneName)
        {
            yield return YandexGamesSdk.Initialize();
            IsConnect.Invoke();
        }
        else if(SceneManager.GetActiveScene().name == ConstantsString.MainMenuSceneName)
        {
            YandexGamesSdk.GameReady();
        }
    }
}
