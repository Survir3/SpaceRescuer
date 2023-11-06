using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatorLevelConfig : MonoBehaviour, ISceneLoadHandler<LevelConfig>
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;

    public LevelConfig LevelConfig { get; private set; }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.GameSceneName)
        {
            _player.IsDie += OnPlayerDead;
            _spawnerSurvivor.IsAllAdded += OnAllAdded;
        }
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.GameSceneName)
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
        if (argument == null)
        {
            ResetLevelConfig();
        }
        else
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
}
