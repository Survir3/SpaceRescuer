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

        IsConnect?.Invoke();
    }
}
