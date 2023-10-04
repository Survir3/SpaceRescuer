using System;
using UnityEngine;
using UnityEngine.Events;

public class Training : MonoBehaviour
{ 
    [SerializeField] private SaverData _saverData;
    [SerializeField] private TimeSceler _timeSceler;
    [SerializeField] private SpawnerArtefact _spawnerArtefact;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private TimerStartLevel _timerStartLevel;

    private int _firstAction = 1;

    public event UnityAction<string> IsTraining;

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

            _timeSceler.PauseGame();

        IsTraining?.Invoke(training);
    }
}
