using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenies : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void OnClickReloadSceneButton()
    {
        if(_player.IsDead)
        {
            Game.Load(0);
        }
        else
        {
            Debug.Log( "ПОИНТЫ" + _player.Points.Value);
            Game.Load(_player.Points.Value);
        }
    }

    public void OnClickLoadSceneButton(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }
}
