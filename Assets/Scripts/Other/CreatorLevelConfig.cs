using Agava.YandexGames;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CreatorLevelConfig : MonoBehaviour, ISceneLoadHandler<LevelConfig>
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private SaverData _saverData;
    [SerializeField] private LoaderCloud _loaderCloud;

    public event UnityAction<LevelConfig> ChangedValueConfig;
    public event UnityAction<int> ChangeValuePoints;

    public LevelConfig LevelConfig { get; private set; }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.MainMenuSceneName)
        {
            _loaderCloud.IsDownloaded += OnDownload;
        }

        if (SceneManager.GetActiveScene().name == ConstantsString.GameSceneName)
        {
            _player.IsDie += OnPlayerDead;
            _spawnerSurvivor.IsAllAdded += OnAllAdded;
        }
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.MainMenuSceneName)
        {
            _loaderCloud.IsDownloaded -= OnDownload;
        }

        if (SceneManager.GetActiveScene().name == ConstantsString.GameSceneName)
        {
            _player.IsDie -= OnPlayerDead;
            _spawnerSurvivor.IsAllAdded -= OnAllAdded;
        }
    }

    private void Start()
    {
        if(PlayerAccount.IsAuthorized==false && _saverData.WasInitializationSaverData)
        {
            ResetLevelConfig();
        }
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        if (argument == null)
        {
            CreateNewConfig();
        }
        else
        {
            LevelConfig = argument;
        }
    }

    private void CreateNewConfig()
    {
        LevelConfig = new LevelConfig();
    }

    private void ResetLevelConfig(SaveJson saveJson)
    {
        LevelConfig = new LevelConfig(saveJson);
    }

    private void ResetLevelConfig()
    {
        LevelConfig =new LevelConfig
            (
            _saverData.CountSurvivorsToLevel,
            _saverData.CountEnemy,
            _saverData.CountArtefact,
            _saverData.SpeedMovement,
            _saverData.PointsPlayer,
            _saverData.TotalTimeToLevel
            );

    }

    private void OnAllAdded()
    {
        LevelConfig.SetPointsConfig(_player.Points.Value);
        LevelConfig.SetConfigForNextLevel();
        ChangedValueConfig?.Invoke(LevelConfig);
        ChangeValuePoints?.Invoke(_player.Points.Value);
    }

    private void OnPlayerDead()
    {
        ChangeValuePoints?.Invoke(_player.Points.Value);
        CreateNewConfig();
        ChangedValueConfig?.Invoke(LevelConfig);
    }

    private void OnDownload(SaveJson saveJson)
    {
        if (saveJson.IsCreatedStruct)
            ResetLevelConfig(saveJson);
    }
}
