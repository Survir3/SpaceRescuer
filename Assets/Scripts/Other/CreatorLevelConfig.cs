using IJunior.TypedScenes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatorLevelConfig : MonoBehaviour, ISceneLoadHandler<LevelConfig>
{
    [SerializeField] private LoaderLeaderboard _loaderLeaderboard;
    [SerializeField] private ConnecterYandex _connecterYandex;
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;

    public LevelConfig LevelConfig;

    public event Action<LevelConfig> IsCreatedData;

    private void OnEnable()
    {
        if (_loaderLeaderboard  && _connecterYandex)
            _loaderLeaderboard.IsLoadFinish += CreateDataLoadScene;

        if(_player && _spawnerSurvivor)
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
        LevelConfig = new LevelConfig();
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        if (_connecterYandex == null)
        {
            LevelConfig = argument;
        }
    }

    private void OnAllAdded()
    {
        LevelConfig.SetPointsConfig(_player.Points.Value);
        LevelConfig.SetConfigForNextLevel();
    }

    private void OnPlayerDead()
    {
        ResetLevelConfig();
    }

    private void CreateDataLoadScene(IReadOnlyList<LeaderPlayerInfo> leaderPlayerInfos)
    {
        LevelConfig = new LevelConfig();
        IsCreatedData?.Invoke(LevelConfig);
    }
}
