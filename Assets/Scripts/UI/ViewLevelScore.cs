using TMPro;
using UnityEngine;

public class ViewLevelScore : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _gameVictoryMenu;
    [SerializeField] private GameObject _viewPoints;
    [SerializeField] private GameObject _finalScore;

    private void Awake()
    {
        _gameOverMenu.SetActive(false);
        _finalScore.SetActive(false);
    }

    private void OnEnable()
    {
        _player.IsDie += OnPlayerDead;
        _spawnerSurvivor.IsAllAdded += OnAllAdded;
    }

    private void OnDisable()
    {
        _player.IsDie -= OnPlayerDead;
        _spawnerSurvivor.IsAllAdded -= OnAllAdded;
    }

    private void OnPlayerDead()
    {
        _viewPoints.SetActive(false);
        _gameOverMenu.SetActive(true);
        _finalScore.SetActive(true);
    }

    private void OnAllAdded()
    {
        _finalScore.SetActive(true);
        _viewPoints.SetActive(false);
        _gameVictoryMenu.SetActive(true);
    }      
}
