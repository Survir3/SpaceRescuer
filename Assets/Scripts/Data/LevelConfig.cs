using UnityEngine;

public class LevelConfig
{
    private int _increaseTimeRewardForVideoAD = 30;
    private int _maxIncreaseCountSurvivorsToLevel = 5;
    private int _maxIncreaseCountEnemy = 5;
    private int _maxIncreaseCountArtefact = 2;
    private float _maxIncreaseSpeed = 0.3f;

    private int _maxCountSurvivorsToLevel = 30;
    private int _maxCountEnemy = 30;
    private int _maxCountArtefact = 10;
    private float _maxSpeedMovement = 4;

    private float _timeToLevel=30;
    private float _timeRewardForVideoAD = 0;
    private float _timeForSurvivor = 3;
    private float _timeBasicToLevel = 20;
    private int _minValue = 1;
    private float _zeroValue = 0f;

    public int CountSurvivorsToLevel { get; private set; } = 6;
    public int CountEnemy { get; private set; } = 15;
    public int CountArtefact { get; private set; } = 2;
    public float SpeedMovement { get; private set; } = 2;
    public int PointsPlayer { get; private set; } = 0;
    public float TotalTimeToLevel => _timeToLevel + _timeRewardForVideoAD;

    public LevelConfig()
    {
    }
    
    public LevelConfig(SaveJson saveJson)
    {
        CountSurvivorsToLevel= saveJson.CountSurvivorsToLevel;
        CountEnemy= saveJson.CountEnemy;
        CountArtefact= saveJson.CountArtefact;
        SpeedMovement= saveJson.SpeedMovement;
        PointsPlayer= saveJson.PointsPlayer;
        _timeToLevel = saveJson.TotalTimeToLevel;
    }
    
    public LevelConfig(int countSurvivorsToLevel, int countEnemy, int countArtefact, float speedMovement, int pointsPlayer, float totalTimeToLevel)
    {
        CountSurvivorsToLevel= countSurvivorsToLevel;
        CountEnemy= countEnemy;
        CountArtefact= countArtefact;
        SpeedMovement= speedMovement;
        PointsPlayer= pointsPlayer;
        _timeToLevel = totalTimeToLevel;
    }

    public void SetConfigForNextLevel()
    {
        _timeRewardForVideoAD = _zeroValue;
        SetIncreaseCountSurvivorsToLevel();
        SetIncreaseCountEnemy();
        SetIncreaseCountArtefact();
        SetIncreaseTimeToLevel(CountSurvivorsToLevel);
        SetIncreaseSpeedMovement();
    }

    public void SetPointsConfig(int value)
    {
        if (value > _zeroValue)
            PointsPlayer = value;
    }

    public void OnSetRewardVideoAD()
    {
        _timeRewardForVideoAD = _increaseTimeRewardForVideoAD;
    }

    private void SetIncreaseCountSurvivorsToLevel()
    {
        CountSurvivorsToLevel += Mathf.Clamp(Random.Range(_minValue, _maxIncreaseCountSurvivorsToLevel), _minValue, _maxCountSurvivorsToLevel);
    }

    private void SetIncreaseCountEnemy()
    {
        CountEnemy += Mathf.Clamp(Random.Range(_minValue, _maxIncreaseCountEnemy), _minValue, _maxCountEnemy);
    }

    private void SetIncreaseCountArtefact()
    {
        CountArtefact += Mathf.Clamp(Random.Range(_minValue, _maxIncreaseCountArtefact), _minValue, _maxCountArtefact);
    }

    private void SetIncreaseTimeToLevel(int countSurvivorToLevel)
    {
        _timeToLevel = _timeBasicToLevel + countSurvivorToLevel * _timeForSurvivor;
    }

    private void SetIncreaseSpeedMovement()
    {
        SpeedMovement += Mathf.Clamp(Random.Range(_zeroValue, _maxIncreaseSpeed), _zeroValue, _maxSpeedMovement);
    }
}
