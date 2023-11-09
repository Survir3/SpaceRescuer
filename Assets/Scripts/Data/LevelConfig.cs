using UnityEngine;

public class LevelConfig
{
    public int CountSurvivorsToLevel { get; private set; } = 6;
    public int CountEnemy { get; private set; } = 15;
    public int CountArtefact { get; private set; } = 2;
    public float TimeToLevel { get; private set; } = 60;
    public float SpeedMovement { get; private set; } = 2;
    public int PointsPlayer { get; private set; } = 0;
    public float TimeRewardForVideoAD { get; private set; } = 0;
    public float TotalTimeToLevel => TimeToLevel + TimeRewardForVideoAD;

    private int _increaseTimeRewardForVideoAD = 30;
    private int _maxIncreaseCountSurvivorsToLevel = 5;
    private int _maxIncreaseCountEnemy = 5;
    private int _maxIncreaseCountArtefact = 2;
    private float _maxIncreaseTimeToLevel = 30;
    private float _maxIncreaseSpeed = 0.3f;

    private int _maxCountSurvivorsToLevel = 30;
    private int _maxCountEnemy = 30;
    private int _maxCountArtefact = 10;
    private float _maxSpeedMovement = 4;

    private float _timeForSurvivor = 15;

    public void SetConfigForNextLevel()
    {
        TimeRewardForVideoAD = 0;
        SetIncreaseCountSurvivorsToLevel();
        SetIncreaseCountEnemy();
        SetIncreaseCountArtefact();
        SetIncreaseTimeToLevel(CountSurvivorsToLevel);
        SetIncreaseSpeedMovement();
    }

    public void SetPointsConfig(int value)
    {
        if (value > 0)
            PointsPlayer = value;
    }

    public void OnSetRewardVideoAD()
    {
        TimeRewardForVideoAD = _increaseTimeRewardForVideoAD;
    }

    private void SetIncreaseCountSurvivorsToLevel()
    {
        CountSurvivorsToLevel += Mathf.Clamp(Random.Range(0, _maxIncreaseCountSurvivorsToLevel), 0, _maxCountSurvivorsToLevel);
    }

    private void SetIncreaseCountEnemy()
    {
        CountEnemy += Mathf.Clamp(Random.Range(0, _maxIncreaseCountEnemy), 0, _maxCountEnemy);
    }

    private void SetIncreaseCountArtefact()
    {
        CountArtefact += Mathf.Clamp(Random.Range(0, _maxIncreaseCountArtefact), 0, _maxCountArtefact);
    }

    private void SetIncreaseTimeToLevel(int countSurvivorToLevel)
    {
        TimeToLevel += countSurvivorToLevel* _timeForSurvivor + TimeRewardForVideoAD;
    }

    private void SetIncreaseSpeedMovement()
    {
        SpeedMovement += Mathf.Clamp(Random.Range(0, _maxIncreaseSpeed), 0, _maxSpeedMovement);
    }
}
