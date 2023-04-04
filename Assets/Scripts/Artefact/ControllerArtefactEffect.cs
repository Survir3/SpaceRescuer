using System.Collections.Generic;
using UnityEngine;

public class ControllerArtefactEffect : MonoBehaviour
{
    private MovementPlayer _playerMovement;
    private Player _player;
    private List<Artefact> _artefacts = new List<Artefact>();

    private void Awake()
    {
        _player= GetComponent<Player>();
        _playerMovement= GetComponent<MovementPlayer>();
    }

    public void AddArtefact(Artefact artefact)
    {
        _artefacts.Add(artefact);
        TryGetEffect(artefact);
    }

    private void TryGetEffect(Artefact artefact)
    {
        switch (artefact)
        {
            case BonusPointsArtefact bonusPoint:
                _player.Points.SetMultiplier(bonusPoint.Multiplier);
                break;
            case SpeedMovementArtefact speedMovement:
                _playerMovement.SetMultiplier(speedMovement.Multiplier);
                break;
            default:
                break;
        }
    }
}
