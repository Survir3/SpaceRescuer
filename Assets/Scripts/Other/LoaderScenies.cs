using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenies : MonoBehaviour
{
    [SerializeField] private SDKInitializer _connecterYandex;
    [SerializeField] private CreatorLevelConfig _creatorLevelConfig;
    [SerializeField] private HandlerRewardAd _adHandlerReward;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.ConectWhisSDKSceneName)
            _connecterYandex.IsConnect += OnLoadMainMenu;
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.ConectWhisSDKSceneName)
            _connecterYandex.IsConnect -= OnLoadMainMenu;
    }

    public void OnClickReloadSceneButton(bool isShowAd)
    {
        if (isShowAd)
        {
            _adHandlerReward.ShowAd(_creatorLevelConfig.LevelConfig.OnSetRewardVideoAD, OnClickLoadGameButton);
        }
        else
        {
            OnClickLoadGameButton();
        }
    }

    public void OnClickLoadGameButton()
    {
        Game.Load(_creatorLevelConfig.LevelConfig);
    }

    public void OnClickLoadMenuButton()
    {
        MainMenu.Load(_creatorLevelConfig.LevelConfig);
    }

    private void OnLoadMainMenu()
    {
        MainMenu.Load(null);
    }
}
