using System;
using UnityEngine;
using TMPro;
using System.Collections;

[Serializable]
public class ViewPoints : MonoBehaviour
{
    [SerializeField] private Points _point;
    [SerializeField] private TMP_Text _textPoints;
    [SerializeField] private TMP_Text _textMult;

    private CanvasGroup _canvasGroup;
    private float _defaultAlpha = 1f;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _canvasGroup= _textMult.GetComponent<CanvasGroup>();
    }

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
    }

    private void StartDurationShowInfo(int multiplier)
    {
        if(_currentCoroutine!=null)
        {
            StopCoroutine( _currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(DurationShowInfo(multiplier));
    }

    private IEnumerator DurationShowInfo(int multiplier)
    {
        float currentDuration = 0;

        _textMult.text="X" + multiplier.ToString();
        _canvasGroup.alpha=_defaultAlpha;

        while (currentDuration<_point.DurationSaveCombo)
        {
            currentDuration += Time.deltaTime;
            _canvasGroup.alpha=1-(currentDuration/ _point.DurationSaveCombo);
            yield return null;
        }
    }
}
