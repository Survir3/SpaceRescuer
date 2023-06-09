using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{    
    [SerializeField] protected float _value;

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
