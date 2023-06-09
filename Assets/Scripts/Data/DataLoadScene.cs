using System.Collections.Generic;
using UnityEngine;

public class DataLoadScene
{
    public LevelConfig LevelConfig;
    public IReadOnlyList<LeaderPlayerInfo> LeaderPlayers;

    public DataLoadScene(LevelConfig levelConfig, IReadOnlyList<LeaderPlayerInfo> leaderPlayerInfo)
    {
        LevelConfig= levelConfig;
        LeaderPlayers= leaderPlayerInfo;
    }
}