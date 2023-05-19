using IJunior.TypedScenes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenies : MonoBehaviour, ISceneLoadHandler<LevelConfig>
{
    [SerializeField] private Player _player;
    [SerializeField] private MovementPlayer _playerMovement;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private SpawnerSurvivor _survivorSpawner;
    [SerializeField] private SpawnerArtefact _artefactSpawmer;
    [SerializeField] private TimerToEndLevel _timerToEndLevel;

    private LevelConfig _levelConfig;

    private void Awake()
    {
        if (_levelConfig == null)
            _levelConfig = new LevelConfig();
    }

    public void OnClickReloadSceneButton()
    {
        if(_player.IsDead)
        {
            _levelConfig= new LevelConfig();
        }
        else
        {
            _levelConfig.SetPointsConfig(_player.Points.Value);
        }

        Game.Load(_levelConfig);
    }

    public void OnClickLoadGameButton()
    {
        Game.Load(_levelConfig);
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        if (argument == null)
        {
            _levelConfig = new LevelConfig();
        }
        else
        { 
            _levelConfig = argument;
        }

        _playerMovement.SetValueToStartLevel(_levelConfig.SpeedMovement);
        _enemySpawner.SetValueToStartLevel(_levelConfig.CountEnemy);
        _survivorSpawner.SetValueToStartLevel(_levelConfig.CountSurvivorsToLevel);
        _artefactSpawmer.SetValueToStartLevel(_levelConfig.CountArtefact);
        _timerToEndLevel.SetValueToStartLevel(_levelConfig.TimeToLevel);
        _player.Points.SetValueToStartLevel(_levelConfig.PointsPlayer);

        _levelConfig.SetConfigForNextLevel();
    }
}
