using Agava.YandexGames;
using IJunior.TypedScenes;
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
        yield return YandexGamesSdk.Initialize();

        Debug.Log("Start");


        PlayerAccount.Authorize(RequestPersonal, Cansel);
    }

    private void RequestPersonal()
    {
        Debug.Log("RequestPersonal");
        PlayerAccount.RequestPersonalProfileDataPermission();
        Debug.Log("RequestPersonal end");
        IsConnect?.Invoke();
    }

    private void Cansel(string error)
    {
        Debug.Log(error);

        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
}
