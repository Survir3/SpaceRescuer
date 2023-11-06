using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ViewPoints : MonoBehaviour
{
    [SerializeField] private Points _point;
    [SerializeField] private TMP_Text _textPoints;
    [SerializeField] private TMP_Text[] _finalScore;
    [SerializeField] private TMP_Text _textCombo;
    [SerializeField] private Image _imageCombo;

    private float _defaultFillImage = 1f;
    private Coroutine _currentCoroutine;    

    private void OnEnable()
    {
        _point.ChangeValue += ShowPoints;
        _point.ChangeMultiplier += StartDurationShowInfo;
    }

    private void OnDisable()
    {
        _point.ChangeValue -= ShowPoints;
        _point.ChangeMultiplier -= StartDurationShowInfo;
    }

    private void ShowPoints(int point)
    {
        _textPoints.text= point.ToString();

        foreach (var score in _finalScore)
        {
            score.text = point.ToString();
        }
    }

    private void StartDurationShowInfo(int multiplier)
    {
        if(_currentCoroutine!=null)
        {
            StopCoroutine( _currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(DurationShowCombo(multiplier));
    }

    private IEnumerator DurationShowCombo(int multiplier)
    {
        float currentDuration = 0;

        _textCombo.text="X" + multiplier.ToString();
        _imageCombo.fillAmount=_defaultFillImage;

        while (currentDuration<_point.DurationSaveCombo)
        {
            currentDuration += Time.deltaTime;
            _imageCombo.fillAmount = _defaultFillImage - (currentDuration/ _point.DurationSaveCombo);
            yield return null;
        }

        _textCombo.text = string.Empty;
    }
}
