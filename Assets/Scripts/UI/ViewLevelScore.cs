using TMPro;
using UnityEngine;

public class ViewLevelScore : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner[] _spawner;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _gameVictoryMenu;
    [SerializeField] private GameObject _viewPoits;
    [SerializeField] private TMP_Text _finalScore;

    private void Awake()
    {
        _gameOverMenu.SetActive(false);
        _finalScore.enabled= false;
    }

    private void OnEnable()
    {
        _player.IsDie += OnPlayerDead;

        foreach (var spawn in _spawner)
        {
            spawn.IsSpawnedFull += OnSpawnerFull;
        }
    }

    private void OnDisable()
    {
        _player.IsDie -= OnPlayerDead;

        foreach (var spawn in _spawner)
        {
            spawn.IsSpawnedFull -= OnSpawnerFull;
        }

    }

    private void OnPlayerDead()
    {
        _finalScore.text=_player.Points.Value.ToString();
        _finalScore.enabled = false;
        _viewPoits.SetActive(false);
        _gameOverMenu.SetActive(true);
    }

    private void OnSpawnerFull()
    {
        _finalScore.text = _player.Points.Value.ToString();
        _finalScore.enabled = false;
        _viewPoits.SetActive(false);
        _gameVictoryMenu.SetActive(true);
    }      
}
