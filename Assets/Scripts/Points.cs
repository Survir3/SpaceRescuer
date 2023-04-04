using System.Collections;
using UnityEngine;

public class Points : MonoBehaviour, IMultiplied
{
    [SerializeField] private float _durationSaveCombo;
    [SerializeField] private int _diminutionMultiplier;

    private int _defaulMultiplier = 1;
    private int _value;
    private int _currentMultiplier = 1;
    private Coroutine _timeComboPoints;
    public int Value=> _value;

    public void Add(int value)
    {
        _value += value* _currentMultiplier;
        СollectCombo();
    }

    public void SetMultiplier(int multiplier)
    {
        if (_currentMultiplier < multiplier)
        {
            _currentMultiplier = multiplier;
        }
        else
        {
            _currentMultiplier += Mathf.Clamp(multiplier - _diminutionMultiplier, 0, int.MaxValue);
        }
    }

    private void СollectCombo()
    {
        if (_timeComboPoints != null)
        {
            StopCoroutine(_timeComboPoints);
        }

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        _currentMultiplier++;
        yield return new WaitForSeconds(_durationSaveCombo);
        _currentMultiplier = _defaulMultiplier;
    }
}
