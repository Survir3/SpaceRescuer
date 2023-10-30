using TMPro;
using UnityEngine;

public class ViewSurvivorAdd : MonoBehaviour
{
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;
    [SerializeField] private TMP_Text _addSurvivors;

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
        _addSurvivors.text = value.ToString() + " / " + _spawnerSurvivor.Count.ToString();
    }
}
