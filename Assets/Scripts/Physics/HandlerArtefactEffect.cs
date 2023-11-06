using UnityEngine;

[RequireComponent (typeof(Player), (typeof(MovementPlayer)))]
public class HandlerArtefactEffect : MonoBehaviour
{
    [SerializeField] private TimerToEndLevel _timerToEndLevel;

    private MovementPlayer _playerMovement;
    private Player _player;

    private void Awake()
    {
        _player= GetComponent<Player>();
        _playerMovement= GetComponent<MovementPlayer>();
    }

    public void GetEffect(Artefact artefact)
    {
        switch (artefact)
        {
            case BonusPointsArtefact bonusPoint:
                bonusPoint.StartEffect(_player.Points);
                break;
            case SpeedMovementArtefact speedMovement:
                speedMovement.StartEffect(_playerMovement.GetAllMovementShake());
                break;
            case TimerArtefact speedMovement:
                speedMovement.StartEffect(_timerToEndLevel);
                break;
            default:
                break;
        }
    }
}
