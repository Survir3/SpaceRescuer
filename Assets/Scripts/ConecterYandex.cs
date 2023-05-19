using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ConecterYandex : MonoBehaviour
{
    [SerializeField] private int _indexMainMenuScene;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
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
