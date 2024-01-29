using Agava.YandexGames;
using TMPro;
using UnityEngine;

public class ViewBestResult : MonoBehaviour
{
    [SerializeField] private LoaderLeaderboard _leaderboard;
    [SerializeField] private SaverData _saverData;
    [SerializeField] private TMP_Text _myRank;

    private void OnEnable()
    {
        _leaderboard.IsLoadUserRank += Show;
    }

    private void OnDisable()
    {
        _leaderboard.IsLoadUserRank -= Show;
    }

    private void Start()
    {
        Show();
    }

    private void Show()
    {
        if(PlayerAccount.IsAuthorized==false)
        {
            _myRank.text = _saverData.BestResult.ToString();
        }
    }

    private void Show(int value)
    {
        _myRank.text = value.ToString();
    }
}
