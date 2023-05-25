using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewSurvivorAdd : MonoBehaviour
{
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private TMP_Text _AddSurvivor;

    private void Start()
    {
        OnAdded(0);
    }

    private void OnEnable()
    {
        _spawnerSurvivor.IsAdded += OnAdded;
    }

    private void OnDisable()
    {
        _spawnerSurvivor.IsAdded -= OnAdded;
    }

    private void OnAdded(int value)
    {
        _AddSurvivor.text=(value + " / " + _spawnerSurvivor.Count).ToString();
    }
}
