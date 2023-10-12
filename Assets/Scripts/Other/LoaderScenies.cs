using Agava.YandexGames;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenies : MonoBehaviour
{
    private const string ConectWhisSDKSceneName = "ConectWhisSDK";

    [SerializeField] private Player _player;
    [SerializeField] private CreatorDataLoadScene _creatorDataLoadScene;
    [SerializeField] private HandlerRewardAd _handlerRewardAd;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name== ConectWhisSDKSceneName)
            _creatorDataLoadScene.IsCreatedData += OnLoadMenuAfterCreatedData;
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == ConectWhisSDKSceneName)
            _creatorDataLoadScene.IsCreatedData -= OnLoadMenuAfterCreatedData;
    }

    public void OnClickReloadSceneButton(bool isShowAd)
    {
         Debug.Log(_creatorDataLoadScene.DataLoadScene.LevelConfig==null);

        if (isShowAd)
        {
            _handlerRewardAd.ShowAd(_creatorDataLoadScene.DataLoadScene.LevelConfig.OnSetRewardVideoAD, OnClickLoadGameButton);
        }
        else
        {
            OnClickLoadGameButton();
        }
    }

    public void OnClickLoadGameButton()
    {
        Game.Load(_creatorDataLoadScene.DataLoadScene);
    }

    public void OnClickLoadMenuButton()
    {
        _creatorDataLoadScene.ResetLevelConfig();

        MainMenu.Load(_creatorDataLoadScene.DataLoadScene);
    }

    private void OnLoadMenuAfterCreatedData(DataLoadScene dataLoadScene)
    {
        Debug.LogError("OnLoadMenuAfterCreatedData " + dataLoadScene.LevelConfig.TimeToLevel);
        MainMenu.Load(dataLoadScene);
    }
}
