using UnityEngine;

public class LeaderPlayerInfo
{
    public string Name { get; private set; }
    public int Score { get; private set; }
    public Texture2D TextureProfile { get; private set; }

    public void Init(string name, int score)
    {
        Name = name;
        Score = score;
    }

    public void Init(string name, int score, Texture2D texture)
    {
        Name= name;
        Score = score;
        TextureProfile = texture;
    }
}
