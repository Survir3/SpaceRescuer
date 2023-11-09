using UnityEngine;

public class HandlerViewPointsCombo : MonoBehaviour
{
    [SerializeField] private GameObject _pointsComboPanel;
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerSurvivor _spawnerSurvivor;

    private void OnEnable()
    {
        _player.IsDie += Disable;
        _spawnerSurvivor.IsAllAdded += Disable;
    }

    private void OnDisable()
    {
        _player.IsDie -= Disable;
        _spawnerSurvivor.IsAllAdded -= Disable;
    }

    private void Disable()
    {
        _pointsComboPanel.SetActive(false);
    }
}
