using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

[Serializable]
public class ViewPoints : MonoBehaviour
{
    [SerializeField] private Points _point;
    [SerializeField] private TMP_Text _textPoints;
    [SerializeField] private TMP_Text _finalScore;
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
    }

    private void ShowPoints(int point)
    {
        _textPoints.text= point.ToString();
        _finalScore.text = point.ToString();
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
            _imageCombo.fillAmount = 1-(currentDuration/ _point.DurationSaveCombo);
            yield return null;
        }

        _textCombo.text = string.Empty;
    }
}
