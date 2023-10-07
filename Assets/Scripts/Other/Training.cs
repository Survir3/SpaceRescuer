using System;
using UnityEngine;
using UnityEngine.Events;

public class Training : MonoBehaviour, INeededSwitchPlayMode
{ 
    [SerializeField] private SaverData _saverData;
    [SerializeField] private SpawnerArtefact _spawnerArtefact;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private TimerStartLevel _timerStartLevel;

    private int _firstAction = 1;

    public bool IsPause { get; set; } = false;

    public event UnityAction<string> IsTraining;
    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    private void OnEnable()
    {
        _saverData.SavedData += OnSavedData;
        _saverData.FirstLoadedGame += OnFirstLoadedGame;
        _timerStartLevel.StartGame += OnStartGame;
    }

    private void OnDisable()
    {
        _saverData.SavedData -= OnSavedData;
        _saverData.FirstLoadedGame -= OnFirstLoadedGame;
        _timerStartLevel.StartGame -= OnStartGame;
    }

    private void OnSavedData(Spawner spawner, string saveDataKey)
    {
        string TrainingInfo="";

        switch (spawner)
        {
            case SpawnerArtefact spawnerArtefact:
                TrainingInfo= ConstantsString.TrainingTextSpawnerArtefact;
                break;
            case SpawnerSurvivor spawnerSurvivor:
                TrainingInfo= ConstantsString.TrainingTextSpawnerSurvivor;
                break;
            case EnemySpawner enemySpawner:
                return;
            default:
                break;
        }

        TryDoTraining(TrainingInfo, saveDataKey);
    }

    private void OnFirstLoadedGame(string saveDataKey)
    {
        TryDoTraining(ConstantsString.TrainingTextFirstLoadGame, saveDataKey);
    }

    private void OnStartGame()
    {
        TryDoTraining(ConstantsString.TrainingTextSpawnerEnemies, ConstantsString.OrderLoadGame);
    }

    private void TryDoTraining(string training, string saveDataKey)
    {
        if (PlayerPrefs.GetInt(saveDataKey) > _firstAction)
            return;

        IsTraining?.Invoke(training);
        RequestPause();
    }

    public void RequestPlay()
    {
        IsPause = false;
        NeededPlay.Invoke();
    }

    public void RequestPause()
    {
        IsPause = true;
        NeededPause.Invoke();
    }    
}
