using UnityEngine;
using UnityEngine.Events;

public class ViewPanelsEndLevel : MonoBehaviour
{
    [SerializeField] private GameObject _gameWinnerMenu;
    [SerializeField] private GameObject _gameOverMenu;

    private GameObject _activeGameMenu;

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

        if (_activeGameMenu != null)
            _activeGameMenu.SetActive(false);
    }
    public void OnClickClosePanelButton()
    {
        if (_activeGameMenu != null)
        { 
            _activeGameMenu.SetActive(true);
        }
    }
}
