using IJunior.TypedScenes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatorDataLoadScene : MonoBehaviour, ISceneLoadHandler<DataLoadScene>
{
    [SerializeField] private LoaderLeaderboard _loaderLeaderboard;
    [SerializeField] private ConnecterYandex _connecterYandex;
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;

    public DataLoadScene DataLoadScene { get; private set; }

    public event Action<DataLoadScene> IsCreatedData;

    private void OnEnable()
    {
        if (_loaderLeaderboard != null && _connecterYandex != null)
            _loaderLeaderboard.IsLoadFinish += CreateDataLoadScene;

        if(_player !=null && _spawnerSurvivor!=null)
        {
            _player.IsDie += OnPlayerDead;
            _spawnerSurvivor.IsAllAdded += OnAllAdded;
        }
    }

    private void OnDisable()
    {
        if (_loaderLeaderboard != null && _connecterYandex != null)
            _loaderLeaderboard.IsLoadFinish -= CreateDataLoadScene;

        if (_player != null && _spawnerSurvivor != null)
        {
            _player.IsDie -= OnPlayerDead;
            _spawnerSurvivor.IsAllAdded -= OnAllAdded;
        }
    }

    public void ResetLevelConfig()
    {
        DataLoadScene.LevelConfig = new LevelConfig();
    }

    public void OnSceneLoaded(DataLoadScene argument)
    {
        if (_loaderLeaderboard == null && _connecterYandex == null)
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            DataLoadScene = argument;
        }
    }

    public void OnLoadNextSceneButton()
    {
        IReadOnlyList<LeaderPlayerInfo> leaderPlayerInfos=new List<LeaderPlayerInfo>();
        LeaderPlayerInfo leaderPlayerInfo = new LeaderPlayerInfo();
        List<LeaderPlayerInfo> test= new List<LeaderPlayerInfo>();
        leaderPlayerInfo.Init("Name", 10);
        test.Add(leaderPlayerInfo);

        DataLoadScene = new DataLoadScene(new LevelConfig(), test);
    }

    private void OnAllAdded()
    {
        DataLoadScene.LevelConfig.SetPointsConfig(_player.Points.Value);
        DataLoadScene.LevelConfig.SetConfigForNextLevel();
    }

    private void OnPlayerDead()
    {
        ResetLevelConfig();
    }

    private void CreateDataLoadScene(IReadOnlyList<LeaderPlayerInfo> leaderPlayerInfos)
    {
        DataLoadScene = new DataLoadScene(new LevelConfig(), leaderPlayerInfos);
        IsCreatedData?.Invoke(DataLoadScene);
    }
}
