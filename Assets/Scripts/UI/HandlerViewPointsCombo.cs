using UnityEngine;

public class HandlerViewPointsCombo : MonoBehaviour
{
    [SerializeField] GameObject _pointsComboPanel;
    [SerializeField] Player _player;
    [SerializeField] SpawnerSurvivor _spawnerSurvivor;

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
