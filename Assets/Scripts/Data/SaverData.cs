using UnityEngine;
using UnityEngine.Events;

public class SaverData : MonoBehaviour
{  
    [SerializeField] private SpawnerArtefact _spawnerArtefact;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private EnemySpawner _spawnerEnemy;
    [SerializeField] private SoundGame _sound;

    public int СountLoadGame { get; private set; }
    public int CountSpawnedSurvivor { get; private set; }
    public int CountSpawnedArtefact { get; private set; }
    public int CountSpawnedEnemy { get; private set; }
    public int OnOrOffSound { get; private set; }

    public event UnityAction<Spawner, string> SavedData;
    public event UnityAction<string> FirstLoadedGame;

    private void Awake()
    {
        ExtractValue();

        if (_spawnerArtefact == null || _spawnerSurvivor == null || _spawnerEnemy == null)
            return;

        PlayerPrefs.SetInt(ConstantsString.OrderLoadGame, ++СountLoadGame);
    }

    private void Start()
    {
        FirstLoadedGame?.Invoke(ConstantsString.OrderLoadGame);

    }

    private void OnEnable()
    {
        AddListener();
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
            case EnemySpawner spawnerEnemy:
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

    private void SaveData(EnemySpawner spawner)
    {
        PlayerPrefs.SetInt(ConstantsString.OrderSpawnedEnemy, ++CountSpawnedEnemy);
        SavedData?.Invoke(spawner, ConstantsString.OrderSpawnedEnemy);
    }

    private void SaveData(bool isPlaySound)
    {
        int trueValue = 1;
        int falseValue = 0;

        if(isPlaySound)
        {
            PlayerPrefs.SetInt(ConstantsString.OrderSoundPlay, trueValue);
        }
        else
        {
            PlayerPrefs.SetInt(ConstantsString.OrderSoundPlay, falseValue);
        }
    }

    private void AddListener()
    {
        _sound.ChangedModePlay += SaveData;

        if (_spawnerArtefact == null || _spawnerSurvivor == null || _spawnerEnemy == null)
            return;

        _spawnerArtefact.IsSpawned += OnAddCount;
        _spawnerSurvivor.IsSpawned += OnAddCount;
        _spawnerEnemy.IsSpawned += OnAddCount;
    }

    private void RemoveListeners()
    {
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
        СountLoadGame += PlayerPrefs.GetInt(ConstantsString.OrderLoadGame);
    }
}
