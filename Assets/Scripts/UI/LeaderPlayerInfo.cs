using UnityEngine;

public class LeaderPlayerInfo
{
    public string Name { get; private set; } = "test";
    public int Score { get; private set; }
    public Texture TextureProfile { get; private set; }

    public void Init(string name, int score)
    {
        Name = name;
        Score = score;
    }

    public void Init(string name, int score, Texture texture)
    {
        Name= name;
        Score = score;
        TextureProfile = texture;
    }
}
