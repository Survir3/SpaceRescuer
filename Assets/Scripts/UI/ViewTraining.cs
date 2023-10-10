using Agava.WebUtility;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewTraining : MonoBehaviour
{
    [SerializeField] private GameObject _trainingPanel;
    [SerializeField] private TMP_Text _trainingText;
    [SerializeField] private Training _training;
    [SerializeField] private TrainingIcon _trainingIcon;
    [SerializeField] private GameObject _templet;
    [SerializeField] private Transform _contaner;
    [SerializeField] private GridLayoutGroup _gridLayout;
    
    private List<GameObject> _icons=new List<GameObject>();
    private Vector2 _startSizeGridLayoutGroup;

    private void Awake()
    {
        _startSizeGridLayoutGroup = _gridLayout.cellSize;
    }

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
                sprites = GetSpriteFirstLoad();
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
        
        ClearContaner();
        SetGridSize(trainingText);

        for (int i = 0; i < sprites.Count; i++)
        {
            GameObject newGameObject= Instantiate(_templet, _contaner);
            _icons.Add(newGameObject);

            Image image = newGameObject.GetComponent<Image>();
            image.sprite = sprites[i];            
        }
    }

    private void ClearContaner()
    {
        foreach (var icon in _icons)
        {
            Destroy(icon.gameObject);
        }
    }

    private IReadOnlyList<Sprite> GetSpriteFirstLoad()
    {
        IReadOnlyList<Sprite> inputIcon;

        if (true)
        {
            inputIcon = _trainingIcon.InputsTouch;
        }
        else
        {
            inputIcon = _trainingIcon.InputsKeyboard;
        }

        return inputIcon;
    }

    private void SetGridSize(string trainingText)
    {
        float multiplierSizeY = 2;
        float multiplierSizeX = 2f;

        if (trainingText == ConstantsString.TrainingTextFirstLoadGame)
        {
            var newSize = new Vector2 (_startSizeGridLayoutGroup.x*multiplierSizeX, _startSizeGridLayoutGroup.y* multiplierSizeY);
            _gridLayout.cellSize = newSize;
        }
        else
        {
            _gridLayout.cellSize = _startSizeGridLayoutGroup;
        }
    }
}
