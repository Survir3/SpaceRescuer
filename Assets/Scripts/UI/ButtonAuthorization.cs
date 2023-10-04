using Agava.YandexGames;
using UnityEngine;

public class ButtonAuthorization : MonoBehaviour
{
    [SerializeField] private LoaderLeaderboard _leaderboard;

    public void OnClick()
    {
        PlayerAccount.Authorize(_leaderboard.LoadEntries);
    }
}
