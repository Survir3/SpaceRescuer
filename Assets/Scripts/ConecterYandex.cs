using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ConecterYandex : MonoBehaviour
{
    public event UnityAction IsAuthorize;

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
        IsAuthorize?.Invoke();
    }
}
