using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ConecterYandex : MonoBehaviour
{
    [SerializeField] private int _indexMainMenuScene;

    public event UnityAction<string> IsLoadYandexSdk;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
        #endif

        yield return YandexGamesSdk.Initialize();

        PlayerAccount.Authorize(RequestPersonal);
    }

    private void RequestPersonal()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(_indexMainMenuScene);
    }
}
