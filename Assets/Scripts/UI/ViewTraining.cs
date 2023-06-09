using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewTraining : MonoBehaviour
{
    [SerializeField] private GameObject _trainingPanel;
    [SerializeField] private TMP_Text _trainingText;
    [SerializeField] private Training _training;
    [SerializeField] private List<Image> images;
    [SerializeField] private TrainingIcon _trainingIcon;


    private void OnEnable()
    {
        _training.IsTraining += OnTraining;
    }

    private void OnDisable()
    {
        _training.IsTraining -= OnTraining;
    }

    private void OnTraining(string trainingText)
    {
        SetText(trainingText);
        SetIcons(trainingText);
        _trainingPanel.SetActive(true);
    }

    private void SetText(string trainingText)
    {
        _trainingText.text = trainingText;
    }

    private IReadOnlyList<Sprite> GetSpriteTraining(string trainingText)
    {
        IReadOnlyList<Sprite> sprites = new List<Sprite>();

        switch (trainingText)
        {
            case ConstantsString.TrainingTextFirstLoadGame:
                sprites = _trainingIcon.Inputs;
                break;
            case ConstantsString.TrainingTextSpawnerSurvivor:
                sprites = _trainingIcon.Survivor;
                break;
            case ConstantsString.TrainingTextSpawnerEnemies:
                sprites = _trainingIcon.Enemies;
                break;
            case ConstantsString.TrainingTextSpawnerArtefact:
                sprites = _trainingIcon.Artefacts;
                break;
            default:
                break;
        }

        return sprites;
    }

    private void SetIcons(string trainingText)
    {
        IReadOnlyList<Sprite> sprites = GetSpriteTraining(trainingText);

        for (int i = 0; i < images.Count; i++)
        {
            if (sprites.Count-1<i)
            {
                images[i].enabled= false;
            }
            else
            {
                images[i].sprite = sprites[i];
                images[i].enabled = true;
            }
        }
    }
}
