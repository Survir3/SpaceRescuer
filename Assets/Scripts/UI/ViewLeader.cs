using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewLeader : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private RawImage _image;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _leanLocalizedText;

    public void SetScore(int score) => _score.text = score.ToString();
    public void SetTexture(Texture2D texture) => _image.texture = texture;

    public void SetName(string name)
    {
        _name.text = name;

        if (_name.text == ConstantsString.Incognito)
        {
            _leanLocalizedText.enabled = true;
        }
    }

    public void InitWithTexture(LeaderPlayerInfo leaderPlayerInfo)
    {
        SetName(leaderPlayerInfo.Name);
        SetScore(leaderPlayerInfo.Score);
        SetTexture(leaderPlayerInfo.TextureProfile);
    }

    public void InitWithoutTexture(LeaderPlayerInfo leaderPlayerInfo)
    {
        SetName(leaderPlayerInfo.Name);
        SetScore(leaderPlayerInfo.Score);
    }
}
