using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoaderCloud : MonoBehaviour
{
    [SerializeField] private SaverData _saverData;
    [SerializeField] private CreatorLevelConfig _creatorLevelConfig;

    public event UnityAction<SaveJson> IsDownloaded;

    private void OnEnable()
    {
        _creatorLevelConfig.ChangedValueConfig += OnChangedValueConfig;
    }

    private void OnDisable()
    {
        _creatorLevelConfig.ChangedValueConfig -= OnChangedValueConfig;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == ConstantsString.MainMenuSceneName)
        {
            Download();
        }
    }

    public void Download()
    {
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.GetCloudSaveData(OnSuccess);
        }
    }

    private void OnChangedValueConfig(LevelConfig levelConfig)
    {
        if (PlayerAccount.IsAuthorized)
        {
            string cloudSaveDataJson = Parse(levelConfig);

            PlayerAccount.GetCloudSaveData(e=>
            {
                if (cloudSaveDataJson != e)
                    PlayerAccount.SetCloudSaveData(cloudSaveDataJson);
            });
        }
    }

    private string Parse(LevelConfig levelConfig)
    {
        SaveJson saveJson = new SaveJson();

        saveJson.CountArtefact =levelConfig.CountArtefact;
        saveJson.SpeedMovement =levelConfig.SpeedMovement;
        saveJson.CountSurvivorsToLevel = levelConfig.CountSurvivorsToLevel;
        saveJson.PointsPlayer = levelConfig.PointsPlayer;
        saveJson.CountEnemy =levelConfig.CountEnemy;
        saveJson.TotalTimeToLevel = levelConfig.TotalTimeToLevel;
        saveJson.IsCreatedStruct = true;

        return JsonUtility.ToJson(saveJson, true);
    }

    private void OnSuccess(string cloudSaveString)
    {
        if(TryParse(cloudSaveString, out SaveJson saveJson))
        {
            IsDownloaded.Invoke(saveJson);
        }
    }

    private bool TryParse(string cloudSaveString, out SaveJson saveJson)
    {
        if (string.IsNullOrEmpty(cloudSaveString))
        {
            saveJson = new SaveJson();
            return false;
        }
        else
        {
            saveJson = JsonUtility.FromJson<SaveJson>(cloudSaveString);
            return true;
        }
    }
}
