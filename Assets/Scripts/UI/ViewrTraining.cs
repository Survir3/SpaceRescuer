using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewrTraining : MonoBehaviour
{
    [SerializeField] private GameObject _trainingPanel;
    [SerializeField] private TMP_Text _trainingText;
    [SerializeField] private Training _training;

    public TMP_Text _trainingTexTestt;
    public SaverData saverData;

    private void Update()
    {
        _trainingTexTestt.text =PlayerPrefs.GetInt("OrderSpawnedSurvivor").ToString();
    }

    private void OnEnable()
    {
        _training.IsTraining += ShowTrainigText;
    }

    private void OnDisable()
    {
        _training.IsTraining -= ShowTrainigText;
    }

    private void ShowTrainigText(string trainingText)
    {
        _trainingPanel.SetActive(true);
        _trainingText.text = trainingText;
    }
}
