using UnityEngine;
using UnityEngine.Events;

public class SaverData : MonoBehaviour
{
    public const string OrderLoadGame = "OrderLoadGame";
    public const string OrderSpawnedSurvivor = "OrderSpawnedSurvivor";
    public const string OrderSpawnedArtefact = "OrderSpawnedArtefact";
    public const string OrderSpawnedEnemy = "OrderSpawnedEnemy";

    [SerializeField] private SpawnerArtefact _spawnerArtefact;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private SpawnerEnemy _spawnerEnemy;


    public int СountLoadGame { get; private set; }
    public int CountSpawnedSurvivor { get; private set; }
    public int CountSpawnedArtefact { get; private set; }
    public int CountSpawnedEnemy { get; private set; }

    public event UnityAction<Spawner, string> SavedData;
    public event UnityAction<string> FIrstLoadedGame;

    private void Awake()
    {
        ExtractValue();
        PlayerPrefs.SetInt(OrderLoadGame, ++СountLoadGame);
    }

    private void Start()
    {
        FIrstLoadedGame?.Invoke(OrderLoadGame);
    }

    private void OnEnable()
    {
        AddListener();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    public bool IsAllDataGreaterZero()
    {
        if(СountLoadGame >0 && CountSpawnedSurvivor > 0 && CountSpawnedArtefact > 0 && CountSpawnedEnemy > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAllDataForTest()
    {
        PlayerPrefs.SetInt(OrderSpawnedArtefact,0);
        PlayerPrefs.SetInt(OrderSpawnedSurvivor,0);
        PlayerPrefs.SetInt(OrderSpawnedEnemy, 0);
        PlayerPrefs.SetInt(OrderLoadGame,0);
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
        PlayerPrefs.SetInt(OrderSpawnedArtefact, ++CountSpawnedArtefact);
        SavedData?.Invoke(spawner, OrderSpawnedArtefact);
    }

    private void SaveData(SpawnerSurvivor spawner)
    {
        PlayerPrefs.SetInt(OrderSpawnedSurvivor, ++CountSpawnedSurvivor);
        SavedData?.Invoke(spawner, OrderSpawnedSurvivor);
    }

    private void SaveData(SpawnerEnemy spawner)
    {
        PlayerPrefs.SetInt(OrderSpawnedEnemy, ++CountSpawnedEnemy);
        SavedData?.Invoke(spawner, OrderSpawnedEnemy);
    }

    private void AddListener()
    {
        if (_spawnerArtefact == null || _spawnerSurvivor == null || _spawnerEnemy == null)
            return;

        _spawnerArtefact.IsSpawned += OnAddCount;
        _spawnerSurvivor.IsSpawned += OnAddCount;
        _spawnerEnemy.IsSpawned += OnAddCount;
    }

    private void RemoveListeners()
    {
        if (_spawnerArtefact == null || _spawnerSurvivor == null || _spawnerEnemy == null)
            return;

        _spawnerArtefact.IsSpawned -= OnAddCount;
        _spawnerSurvivor.IsSpawned -= OnAddCount;
        _spawnerEnemy.IsSpawned -= OnAddCount;
    }

    private void ExtractValue()
    {
        CountSpawnedArtefact=PlayerPrefs.GetInt(OrderSpawnedArtefact);
        CountSpawnedSurvivor= PlayerPrefs.GetInt(OrderSpawnedSurvivor);
        CountSpawnedEnemy=PlayerPrefs.GetInt(OrderSpawnedEnemy);
        СountLoadGame += PlayerPrefs.GetInt(OrderLoadGame);
    }
}
