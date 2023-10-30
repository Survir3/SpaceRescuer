using UnityEngine;
using UnityEngine.Events;

public class HandlerButtonExitGame : MonoBehaviour, INeededSwitchPlayMode, INeededSwitchSoundPlay
{
    [SerializeField] private GameObject _gameWinnerMenu;
    [SerializeField] private GameObject _gameOverMenu;

    private GameObject _activeGameMenu;
    public bool IsPause { get; private set; }
    public bool IsOffSound { get; private set; }

    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;
    public event UnityAction NeededOffSound;
    public event UnityAction NeededOnSound;

    public void OnClickOpenPanelButton()
    {
        if (_gameWinnerMenu.activeSelf)
        {
            _activeGameMenu = _gameWinnerMenu;
        }
        else if (_gameOverMenu.activeSelf)
        {
            _activeGameMenu = _gameOverMenu;
        }
        else
        {
            _activeGameMenu = null;
        }

        RequestPause();
        RequestOffSound();

        if (_activeGameMenu != null)
            _activeGameMenu.SetActive(false);
    }
    public void OnClickClosePanelButton()
    {
        RequestOnSound();
        RequestPlay();

        if (_activeGameMenu != null)
            _activeGameMenu.SetActive(true);
    }

    public void RequestPlay()
    {
        IsPause = false;
        NeededPlay.Invoke();
    }

    public void RequestPause()
    {
        IsPause = true;
        NeededPause.Invoke();
    }

    public void RequestOffSound()
    {
        IsOffSound = true;
        NeededOffSound.Invoke();
    }

    public void RequestOnSound()
    {
        IsOffSound = false;
        NeededOnSound.Invoke();
    }
}
