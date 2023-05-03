using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private TMP_Text _finalScore;

    private void Awake()
    {
        Time.timeScale = 1;
        _mainMenu.SetActive(false);
    }

    private void OnEnable()
    {
        _player.IsDead += OnPlayerDead;    
    }

    private void OnDisable()
    {
        _player.IsDead -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        Time.timeScale= 0;
        _finalScore.text=_player.Points.Value.ToString();
        _mainMenu.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("sadg");
        SceneManager.LoadSceneAsync(0);
    }
}
