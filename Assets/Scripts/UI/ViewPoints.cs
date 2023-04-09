using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    [Serializable]
public class ViewPoints : MonoBehaviour
{
    [SerializeField] private Points _point;
    [SerializeField] private Text _textPoints;
    [SerializeField] private Text _textMult;

    private void OnEnable()
    {
        _point.ChangeValue += ShowPoints;
    }

    private void OnDisable()
    {
        _point.ChangeValue -= ShowPoints;
    }

    private void Update()
    {
        _textMult.text=_point.CurrentMultiplier.ToString();
    }

    private void ShowPoints(int point)
    {
        _textPoints.text= point.ToString();
    }
}
