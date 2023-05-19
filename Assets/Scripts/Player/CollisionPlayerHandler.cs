using UnityEngine;

[RequireComponent ((typeof(Player)), (typeof(MovementPlayer)), typeof(ControllerSurvivorMovement))]
public class CollisionPlayerHandler : MonoBehaviour
{
    private Player _player;
    private ControllerSurvivorMovement _controllerSurvivorMovement;
    private ControllerArtefactEffect _controllerArtefactEffect;
    
    private void Awake()
    {
        _player= GetComponent<Player>();
        _controllerSurvivorMovement= GetComponent<ControllerSurvivorMovement>();
        _controllerArtefactEffect = GetComponent<ControllerArtefactEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<CollisionEnemyHandler>(out CollisionEnemyHandler enemyHandler))
        {
            _player.Dead();
            return;
        }
        else if (other.gameObject.TryGetComponent<CollisionSurvivorHandler>(out CollisionSurvivorHandler survivorHandler))
        {
            if (survivorHandler.IsAdded)
            {
                _player.Dead();
                return;
            }
            else
            {
                survivorHandler.AddInSnake();
                survivorHandler.GetComponent<Survivor>().GivePoints(_player.Points);
                 _controllerSurvivorMovement.AddSurvivor(survivorHandler.GetComponent<SurvivorMovement>());
            }
        }
        else if(other.gameObject.TryGetComponent<CollisionArtefactHandler>(out CollisionArtefactHandler artefactHandler))
        {
            if (artefactHandler.IsAdded)
            {
                return;
            }
            else
            {
                artefactHandler.AddInSnake();
                Artefact artefact = artefactHandler.GetComponentInParent<Artefact>();
                artefact.GivePoints(_player.Points);
                _controllerArtefactEffect.TryGetEffect(artefact);
            }
        }
    }
}
