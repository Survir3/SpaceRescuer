using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{    
    [SerializeField] protected float _value;

    protected float _startValue;

    public IEnumerator Countdown(Action<float> intermediateValue=null, Action before=null, Action after=null)
    {
        before?.Invoke();

        while (_value > 0)
        {
            _value -= Time.deltaTime;
            intermediateValue?.Invoke(_value);
            yield return new WaitForEndOfFrame();
        }

        after?.Invoke();
    }
}
