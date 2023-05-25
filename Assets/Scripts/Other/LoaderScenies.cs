using Agava.YandexGames;
using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

public class LoaderScenies : MonoBehaviour, ISceneLoadHandler<LevelConfig>
{
    [SerializeField] private Player _player;
    [SerializeField] private MovementPlayer _playerMovement;
    [SerializeField] private SpawnerEnemy _enemySpawner;
    [SerializeField] private SpawnerSurvivor _survivorSpawner;
    [SerializeField] private SpawnerArtefact _artefactSpawmer;
    [SerializeField] private TimerToEndLevel _timerToEndLevel;
    [SerializeField] private TimeSceler _timeSceler;
    [SerializeField] private LoaderLeaderboard _loaderLeaderboard;

    private LevelConfig _levelConfig;

    private void OnEnable()
    {
        if (_loaderLeaderboard != null)
            _loaderLeaderboard.IsLoadFinish += OnFirstLoadMenu;
    }

    private void OnDisable()
    {
        if (_loaderLeaderboard != null)
            _loaderLeaderboard.IsLoadFinish -= OnFirstLoadMenu;
    }

    public void OnClickReloadSceneButton(bool isShowAd)
    {
        if (isShowAd)
        {
            VideoAd.Show(_timeSceler.PauseGame, _levelConfig.OnSetRewardVideoAD, LoadGameForVideoAD);
        }
        else
        {
            if (_player.IsDead)
            {
                _levelConfig = new LevelConfig();
            }
            else
            {
                _levelConfig.SetPointsConfig(_player.Points.Value);
            }

            Game.Load(_levelConfig);
        }
    }

    public void OnClickLoadGameButton()
    {
        Game.Load(_levelConfig);
    }

    public void OnClickLoadMenuButton()
    {
        MainMenu.Load(_levelConfig);
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        if (_player == null)
            return;

        if (argument == null)
        {
            _levelConfig = new LevelConfig();
        }
        else
        { 
            _levelConfig = argument;
        }

        SetConfigToLevel(_levelConfig);
        _levelConfig.SetConfigForNextLevel();
    }

    private void SetConfigToLevel(LevelConfig levelConfig)
    {
        _playerMovement.SetValueToStartLevel(levelConfig.SpeedMovement);
        _enemySpawner.SetValueToStartLevel(levelConfig.CountEnemy);
        _survivorSpawner.SetValueToStartLevel(levelConfig.CountSurvivorsToLevel);
        _artefactSpawmer.SetValueToStartLevel(levelConfig.CountArtefact);
        _timerToEndLevel.SetValueToStartLevel(levelConfig.TimeToLevel);
        _player.Points.SetValueToStartLevel(levelConfig.PointsPlayer);
    }

    private void LoadGameForVideoAD()
    {
        Game.Load(_levelConfig);
    }

    private void OnFirstLoadMenu(IReadOnlyList<LeaderPlayerInfo> leaderPlayerInfos)
    {
        MainMenu.Load(leaderPlayerInfos);
    }
}
