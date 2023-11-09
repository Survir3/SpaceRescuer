using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaverData : MonoBehaviour
{  
    [SerializeField] private SpawnerArtefact _spawnerArtefact;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private SpawnerEnemy _spawnerEnemy;
    [SerializeField] private HandlerSound _sound;

    public event UnityAction<Spawner, string> SavedData;
    public event UnityAction<string> FirstLoadedGame;

    public int СountLoadGame { get; private set; }
    public int CountSpawnedSurvivor { get; private set; }
    public int CountSpawnedArtefact { get; private set; }
    public int CountSpawnedEnemy { get; private set; }
    public bool OffSound { get; private set; }


    private void Awake()
    {
        ExtractValue();

        if (SceneManager.GetActiveScene().name == ConstantsString.GameSceneName)
            PlayerPrefs.SetInt(ConstantsString.OrderLoadGame, ++СountLoadGame);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.GameSceneName)
            FirstLoadedGame?.Invoke(ConstantsString.OrderLoadGame);
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    public void ResetAllDataForTest()
    {
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedArtefact,0);
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedSurvivor,0);
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedEnemy, 0);
        PlayerPrefs.SetInt(ConstantsString.OrderLoadGame,0);
        PlayerPrefs.SetInt(ConstantsString.OrderSoundPlay, 0);
    }

    private void OnAddCount(Spawner spawner)
    {
        switch (spawner)
        {
            case SpawnerArtefact spawnerArtefact:
                SaveData(spawnerArtefact); 
                    break;
            case SpawnerSurvivor spawnerSurvivor:
                SaveData(spawnerSurvivor);
                break;
            case SpawnerEnemy spawnerEnemy:
                SaveData(spawnerEnemy);
                break;
            default:
                break;
        }
    }

    private void SaveData(SpawnerArtefact spawner)
    {
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedArtefact, ++CountSpawnedArtefact);
        SavedData?.Invoke(spawner, ConstantsString.OrderSpawnedArtefact);
    }

    private void SaveData(SpawnerSurvivor spawner)
    {
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedSurvivor, ++CountSpawnedSurvivor);
        SavedData?.Invoke(spawner, ConstantsString.OrderSpawnedSurvivor);
    }

    private void SaveData(SpawnerEnemy spawner)
    {
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedEnemy, ++CountSpawnedEnemy);
        SavedData?.Invoke(spawner, ConstantsString.OrderSpawnedEnemy);
    }

    private void SaveData(bool isOffSound)
    {
        PlayerPrefs.SetInt(ConstantsString.OrderSoundPlay, Convert.ToInt32(isOffSound));
    }

    private void AddListeners()
    {
        if (_sound!=null)
        _sound.ChangedModePlay += SaveData;

        if (_spawnerArtefact == null || _spawnerSurvivor == null || _spawnerEnemy == null)
            return;

        _spawnerArtefact.IsSpawned += OnAddCount;
        _spawnerSurvivor.IsSpawned += OnAddCount;
        _spawnerEnemy.IsSpawned += OnAddCount;
    }

    private void RemoveListeners()
    {
        if (_sound != null)
            _sound.ChangedModePlay -= SaveData;

        if (_spawnerArtefact == null || _spawnerSurvivor == null || _spawnerEnemy == null)
            return;

        _spawnerArtefact.IsSpawned -= OnAddCount;
        _spawnerSurvivor.IsSpawned -= OnAddCount;
        _spawnerEnemy.IsSpawned -= OnAddCount;
    }

    private void ExtractValue()
    {
        CountSpawnedArtefact=PlayerPrefs.GetInt(ConstantsString.OrderSpawnedArtefact);
        CountSpawnedSurvivor= PlayerPrefs.GetInt(ConstantsString.OrderSpawnedSurvivor);
        CountSpawnedEnemy=PlayerPrefs.GetInt(ConstantsString.OrderSpawnedEnemy);
        СountLoadGame = PlayerPrefs.GetInt(ConstantsString.OrderLoadGame);
        OffSound= Convert.ToBoolean(PlayerPrefs.GetInt(ConstantsString.OrderSoundPlay));
    }
}