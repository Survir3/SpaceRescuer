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

    private void Awake()
    {
        _leanLocalizedText.enabled = false;
    }

    public void SetName(string name)
    {
        Debug.Log(name == ConstantsString.Incognito);

        if (name == ConstantsString.Incognito)
        {
            _leanLocalizedText.enabled = true;
        }

        _name.text = name;
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
