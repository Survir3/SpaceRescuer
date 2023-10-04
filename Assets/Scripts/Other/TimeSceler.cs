using Agava.WebUtility;
using UnityEngine;

public class TimeSceler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;

    private bool _isPause = false;

    private void Awake()
    {
        PlayGame();
    }

    private void OnEnable()
    {
        _player.IsDie += PauseGame;
        _spawnerSurvivor.IsAllAdded+= PauseGame;
        WebApplication.InBackgroundChangeEvent += OnBackgroundChangeEvent;
    }


    private void OnDisable()
    {
        _player.IsDie -= PauseGame;
        _spawnerSurvivor.IsAllAdded -= PauseGame;
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChangeEvent;
    }

    private void OnBackgroundChangeEvent(bool hidden)
    {
        if (_player.IsDead || _spawnerSurvivor.IsAllAdd || _isPause)
            return;

        if (hidden)
            PauseGame();
        else
            PlayGame();
    }

    public void OnClickPauseButton()
    {
        if (_player.IsDead || _spawnerSurvivor.IsAllAdd)
            return;

        if (_isPause)
            PlayGame();
        else
            PauseGame();

        _isPause = !_isPause;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
    }
}
