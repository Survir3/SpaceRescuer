using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Points : MonoBehaviour, IMultiplied
{
    [SerializeField] private float _durationSaveCombo;
    [SerializeField] private int _diminutionMultiplier;

    private int _defaulMultiplier = 1;
    private int _value;
    private int _currentMultiplier = 0;
    private Coroutine _timeComboPoints;

    public int Value=> _value;
    public int CurrentMultiplier => _currentMultiplier;
    public float DurationSaveCombo => _durationSaveCombo;

    public event UnityAction<int> ChangeValue;
    public event UnityAction<int> ChangeMultiplier;


    public void Add(int value)
    {
        СollectCombo();
        _value += value* _currentMultiplier;
        ChangeValue?.Invoke(_value);
        Debug.Log("add");
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

        ChangeMultiplier?.Invoke(_currentMultiplier);
    }

    private void СollectCombo()
    {
        if (_timeComboPoints != null)
        {
            StopCoroutine(_timeComboPoints);
        }

        _timeComboPoints=StartCoroutine(TimerCombo());
    }

    private IEnumerator TimerCombo()
    {
        _currentMultiplier++;
        ChangeMultiplier?.Invoke(_currentMultiplier);
        yield return new WaitForSeconds(_durationSaveCombo);
        SetDefaultMultiplier();
    }

    public void SetDefaultMultiplier()
    {
        _currentMultiplier = _defaulMultiplier;
    }
}
