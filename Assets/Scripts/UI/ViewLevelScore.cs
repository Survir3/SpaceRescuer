using TMPro;
using UnityEngine;

public class ViewLevelScore : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _gameVictoryMenu;

    private void Awake()
    {
        _gameOverMenu.SetActive(false);
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
        _gameOverMenu.SetActive(true);
    }

    private void OnAllAdded()
    {
        _gameVictoryMenu.SetActive(true);
    }      
}
