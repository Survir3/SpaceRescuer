using UnityEngine;
using UnityEngine.Events;

public class Training : MonoBehaviour
{
    private const string FirstLoadGame = "Привет. Для поворота используй стрелочки, A и D или лувую и правую сторону экрана телефона." +
                                         "Ты должен успеть собрать нужное количество выживших за отведенное время." +
                                         "Эту информацию ты можешь увидеть вверху экрана";
    private const string SpawnerArtefact = "На планете появился артефакт. Артефакты дают различные бонусы, такие как:" +
                                           "ускрение на небольшой промежуток времени," +
                                           "дополнительное увеличение множителя очков." +
                                           "Также каждый артефакт дает очки.";
    private const string SpawnerSurvivor = "Появился первый выживший! Скорее беги к нему, за его спасение ты получишь очки, " +
                                           "а если за короткий промежуток времени, ты спасешь нескольких - получишь дополнительные очки.";


    [SerializeField] private SaverData _saverData;
    [SerializeField] private TimeSceler _timeSceler;
    [SerializeField] private SpawnerArtefact _spawnerArtefact;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;

    private int _firstAction = 1;

    public event UnityAction<string> IsTraining;

    private void Awake()
    {
        if(_saverData.IsAllDataGreaterZero())
            enabled = false;
    }

    private void OnEnable()
    {
        _saverData.SavedData += OnSavedData;
        _saverData.FIrstLoadedGame += OnFirstLoadedGame;
    }

    private void OnDisable()
    {
        _saverData.SavedData -= OnSavedData;
        _saverData.FIrstLoadedGame -= OnFirstLoadedGame;
    }

    private void OnSavedData(Spawner spawner, string saveDataKey)
    {
        string TrainingInfo="";

        switch (spawner)
        {
            case SpawnerArtefact spawnerArtefact:
                TrainingInfo=SpawnerArtefact;
                break;
            case SpawnerSurvivor spawnerSurvivor:
                TrainingInfo=SpawnerSurvivor;
                break;
            default:
                break;
        }

        TryDoTraining(TrainingInfo, saveDataKey);
    }

    private void OnFirstLoadedGame(string saveDataKey)
    {
        TryDoTraining(FirstLoadGame, saveDataKey);
    }

    private void TryDoTraining(string training, string saveDataKey)
    {
        if (PlayerPrefs.GetInt(saveDataKey) > _firstAction)
            return;

            _timeSceler.PauseGame();
            IsTraining?.Invoke(training);
    }
}
