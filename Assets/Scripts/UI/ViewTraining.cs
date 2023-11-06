using Agava.WebUtility;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewTraining : MonoBehaviour
{
    [SerializeField] private GameObject _trainingPanel;
    [SerializeField] private Training _training;
    [SerializeField] private TrainingIcon _trainingIcon;
    [SerializeField] private GameObject _templet;
    [SerializeField] private Transform _contaner;
    [SerializeField] private GridLayoutGroup _gridLayout;
    [SerializeField] private List<TMP_Text> _trainingTexts;

    private static int SizeX = 150;
    private static int SizeY = 150;

    private List<GameObject> _icons=new List<GameObject>();
    private Vector2 _startSizeGridLayoutGroup= new Vector2(SizeX, SizeY);
    private TMP_Text _currentText;

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
        foreach (var text in _trainingTexts)
        {
            if (text.text == trainingText)
            {
                SetText(text);
            }
        }

        SetIcons(trainingText);
        _trainingPanel.SetActive(true);

    }

    private void SetText(TMP_Text trainingText)
    {
        if (_currentText != null)
            _currentText.gameObject.SetActive(false);

        _currentText = trainingText;
        trainingText.gameObject.SetActive(true);
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
            Destroy(icon);
        }
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

    private IReadOnlyList<Sprite> GetSpriteFirstLoad()
    {
        IReadOnlyList<Sprite> inputIcon;

        if (Device.IsMobile)
            inputIcon = _trainingIcon.InputsTouch;
        else
            inputIcon = _trainingIcon.InputsKeyboard;

        return inputIcon;
    }

    private void SetGridSize(string trainingText)
    {
        float multiplierSizeY = 2;
        float multiplierSizeX = 2f;

        if (trainingText == ConstantsString.TrainingTextFirstLoadGame && Device.IsMobile)
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
