using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

        PlayerAccount.Authorize(RequestPersonal);
    }

    private void RequestPersonal()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
        IsConnect?.Invoke();
    }
}
