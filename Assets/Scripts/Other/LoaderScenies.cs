using Agava.YandexGames;
using IJunior.TypedScenes;
using UnityEngine;

public class LoaderScenies : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TimeSceler _timeSceler;
    [SerializeField] private CreatorDataLoadScene _creatorDataLoadScene;

    private void OnEnable()
    {
        if (_creatorDataLoadScene != null)
            _creatorDataLoadScene.IsCreatedData += OnLoadMenuAfterCreatedData;
    }

    private void OnDisable()
    {
        if (_creatorDataLoadScene != null)
            _creatorDataLoadScene.IsCreatedData -= OnLoadMenuAfterCreatedData;
    }

    public void OnClickReloadSceneButton(bool isShowAd)
    {
        if (isShowAd)
        {
            VideoAd.Show(_timeSceler.PauseGame, _creatorDataLoadScene.DataLoadScene.LevelConfig.OnSetRewardVideoAD, OnClickLoadGameButton);
        }
        else
        {    
            Game.Load(_creatorDataLoadScene.DataLoadScene);
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
        MainMenu.Load(dataLoadScene);
    }
}
