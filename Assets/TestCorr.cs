using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCorr : MonoBehaviour
{
    Coroutine _corr;
    void Start()
    {
        _corr = StartCoroutine(Test());
    }

    private void Update()
    {
        Debug.Log(_corr == null);
    }

    private IEnumerator Test()
    {
        Debug.Log("старт");
        yield return new WaitForSeconds(5);
        Debug.Log("енд");
    }
}
