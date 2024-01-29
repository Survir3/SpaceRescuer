using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{    
    [SerializeField] protected float Value;

    protected float StartValue;

    public IEnumerator Countdown(Action<float> intermediateValue=null, Action before=null, Action after=null)
    {
        before?.Invoke();

        while (Value > 0)
        {
            Value -= Time.deltaTime;
            intermediateValue?.Invoke(Value);
            yield return new WaitForEndOfFrame();
        }

        after?.Invoke();
    }

    public IEnumerator Countdown(Action<int> after=null, int valueAfter = 0)
    {
        while (Value > 0)
        {
            Value -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        after?.Invoke(valueAfter);
    }
}
