using Agava.YandexGames;
using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenies : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CreatorLevelConfig _creatorDataLoadScene;
    [SerializeField] private HandlerRewardAd _handlerRewardAd;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.ConectWhisSDKSceneName)
            _creatorDataLoadScene.IsCreatedData += OnLoadMenuAfterCreatedData;
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.ConectWhisSDKSceneName)
            _creatorDataLoadScene.IsCreatedData -= OnLoadMenuAfterCreatedData;
    }
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }


    private void Start()
    {
    }

    public void OnClickReloadSceneButton(bool isShowAd)
    {
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

    private void OnLoadMenuAfterCreatedData(LevelConfig dataLoadScene)
    {
        MainMenu.Load(dataLoadScene);
    }
}
