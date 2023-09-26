using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewSurvivorAdd : MonoBehaviour
{
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private TMP_Text _addSurvivor;
    [SerializeField] private TMP_Text _maxSurvivor;

    private void Start()
    {
        OnAdded(0);
        _maxSurvivor.text = " / " + _spawnerSurvivor.Count.ToString();
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
        _addSurvivor.text=value.ToString();
    }
}
