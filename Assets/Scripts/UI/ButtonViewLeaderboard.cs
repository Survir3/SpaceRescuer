using Agava.YandexGames;
using UnityEngine;

public class ButtonViewLeaderboard : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboard;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _authorization;

    private void Awake()
    {
        Close();
    }

    public void OnClick()
    {
        if (PlayerAccount.IsAuthorized)
        {
            Open();
        }
        else
        {
            _authorization.SetActive(true);
        }
    }

    private void Open()
    {
        _leaderboard.SetActive(true);
        _mainMenu.SetActive(false);
    }

    private void Close()
    {
        _leaderboard.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
