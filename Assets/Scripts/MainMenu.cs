using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _indexGameScene;

    public void StartGame()
    {
        SceneManager.LoadScene(_indexGameScene);
    }
}
