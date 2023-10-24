using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ConnecterYandex : MonoBehaviour
{
    public event UnityAction IsConnect;

    private void Awake()
    {        
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.ConectWhisSDKSceneName)
        {
            yield return YandexGamesSdk.Initialize();
            YandexGamesSdk.GameReady();
            IsConnect.Invoke();
        }
    }
}
