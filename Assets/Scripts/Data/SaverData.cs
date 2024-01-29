using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayerPrefs = UnityEngine.PlayerPrefs;

public class SaverData : MonoBehaviour
{
    [SerializeField] private SpawnerArtefact _spawnerArtefact;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private SpawnerEnemy _spawnerEnemy;
    [SerializeField] private HandlerSound _sound;
    [SerializeField] private Player _player;
    [SerializeField] private CreatorLevelConfig _creatorLevelConfig;

    public event UnityAction<Spawner, string> SavedData;
    public event UnityAction<string> FirstLoadedGame;

    public int CountSurvivorsToLevel { get; private set; }
    public int CountEnemy { get; private set; }
    public int CountArtefact { get; private set; }
    public float SpeedMovement { get; private set; }
    public int PointsPlayer { get; private set; }
    public float TotalTimeToLevel { get; private set; }
    public int СountLoadGame { get; private set; }
    public int CountSpawnedSurvivor { get; private set; }
    public int CountSpawnedArtefact { get; private set; }
    public int CountSpawnedEnemy { get; private set; }
    public bool OffSound { get; private set; }
    public int BestResult { get; private set; }
    public bool WasInitializationSaverData { get; private set; } = false;

    private void Awake()
    {
        ExtractGameValue();
        ExtractPlayerValue();

        if (SceneManager.GetActiveScene().name == ConstantsString.GameSceneName)
            PlayerPrefs.SetInt(ConstantsString.OrderLoadGame, ++СountLoadGame);

        if (SceneManager.GetActiveScene().name == ConstantsString.MainMenuSceneName)
        {
            WasInitializationSaverData=Convert.ToBoolean(PlayerPrefs.GetInt(ConstantsString.InitializationSaverData));
        }
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
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedArtefact, 0);
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedSurvivor, 0);
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedEnemy, 0);
        PlayerPrefs.SetInt(ConstantsString.OrderLoadGame, 0);
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

    private void SaveBestResultPlayer()
    {
        int savedBestResult = PlayerPrefs.GetInt(ConstantsString.BestResult);

        if (_player.Points.Value > savedBestResult)
        {
            BestResult = _player.Points.Value;
            PlayerPrefs.SetInt(ConstantsString.BestResult, _player.Points.Value);
        }
    }

    private void SaveData(LevelConfig levelConfig)
    {   
        if(PlayerAccount.IsAuthorized==false)
        {
        CountSurvivorsToLevel = levelConfig.CountSurvivorsToLevel;
        PlayerPrefs.SetInt(ConstantsString.CountSurvivorsToLevel, CountSurvivorsToLevel);

        CountEnemy = levelConfig.CountEnemy;
        PlayerPrefs.SetInt(ConstantsString.CountEnemy, CountEnemy);

        CountArtefact = levelConfig.CountArtefact;
        PlayerPrefs.SetInt(ConstantsString.CountArtefact, CountArtefact);

        SpeedMovement = levelConfig.SpeedMovement;
        PlayerPrefs.SetFloat(ConstantsString.SpeedMovement, SpeedMovement);

        PointsPlayer = levelConfig.PointsPlayer;
        PlayerPrefs.SetInt(ConstantsString.PointsPlayer, PointsPlayer);

        TotalTimeToLevel = levelConfig.TotalTimeToLevel;
        PlayerPrefs.SetFloat(ConstantsString.TotalTimeToLevel, TotalTimeToLevel);

        WasInitializationSaverData = true;
        PlayerPrefs.SetInt(ConstantsString.InitializationSaverData, Convert.ToInt32(WasInitializationSaverData));

        SaveBestResultPlayer();
        }
    }

    private void AddListeners()
    {
        if (_creatorLevelConfig != null)
            _creatorLevelConfig.ChangedValueConfig += SaveData;

        if (_sound != null)
            _sound.ChangedModePlay += SaveData;

        if (_spawnerArtefact == null || _spawnerSurvivor == null || _spawnerEnemy == null)
            return;

        _spawnerArtefact.IsSpawned += OnAddCount;
        _spawnerSurvivor.IsSpawned += OnAddCount;
        _spawnerEnemy.IsSpawned += OnAddCount;
    }

    private void RemoveListeners()
    {
        if (_creatorLevelConfig != null)
            _creatorLevelConfig.ChangedValueConfig -= SaveData;

        if (_sound != null)
            _sound.ChangedModePlay -= SaveData;

        if (_spawnerArtefact == null || _spawnerSurvivor == null || _spawnerEnemy == null)
            return;

        _spawnerArtefact.IsSpawned -= OnAddCount;
        _spawnerSurvivor.IsSpawned -= OnAddCount;
        _spawnerEnemy.IsSpawned -= OnAddCount;

    }

    private void ExtractGameValue()
    {
        CountSpawnedArtefact = PlayerPrefs.GetInt(ConstantsString.OrderSpawnedArtefact);
        CountSpawnedSurvivor = PlayerPrefs.GetInt(ConstantsString.OrderSpawnedSurvivor);
        CountSpawnedEnemy = PlayerPrefs.GetInt(ConstantsString.OrderSpawnedEnemy);
        СountLoadGame = PlayerPrefs.GetInt(ConstantsString.OrderLoadGame);
        OffSound = Convert.ToBoolean(PlayerPrefs.GetInt(ConstantsString.OrderSoundPlay));
    }

    private void ExtractPlayerValue()
    {
        BestResult = PlayerPrefs.GetInt(ConstantsString.BestResult);
        CountSurvivorsToLevel = PlayerPrefs.GetInt(ConstantsString.CountSurvivorsToLevel);
        CountEnemy = PlayerPrefs.GetInt(ConstantsString.CountEnemy);
        CountArtefact = PlayerPrefs.GetInt(ConstantsString.CountArtefact);
        SpeedMovement = PlayerPrefs.GetFloat(ConstantsString.SpeedMovement);
        PointsPlayer = PlayerPrefs.GetInt(ConstantsString.PointsPlayer);
        TotalTimeToLevel = PlayerPrefs.GetFloat(ConstantsString.TotalTimeToLevel);
        WasInitializationSaverData = Convert.ToBoolean(PlayerPrefs.GetInt(ConstantsString.InitializationSaverData));
    }
}