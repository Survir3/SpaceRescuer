using UnityEngine;

[RequireComponent ((typeof(Player)), (typeof(MovementPlayer)), typeof(GiverTargetSurvivorMovement))]
public class CollisionPlayerHandler : MonoBehaviour
{
    private Player _player;
    private GiverTargetSurvivorMovement _controllerSurvivorMovement;
    private HandlerArtefactEffect _controllerArtefactEffect;
    
    private void Awake()
    {
        _player= GetComponent<Player>();
        _controllerSurvivorMovement= GetComponent<GiverTargetSurvivorMovement>();
        _controllerArtefactEffect = GetComponent<HandlerArtefactEffect>();
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
                survivorHandler.GetComponent<Survivor>().GivePoints(_player.Points);
                 _controllerSurvivorMovement.AddSurvivor(survivorHandler.GetComponent<SurvivorMovement>());
                survivorHandler.AddInSnake();
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
                Artefact artefact = artefactHandler.GetComponentInParent<Artefact>();
                artefact.GivePoints(_player.Points);
                _controllerArtefactEffect.GetEffect(artefact);
                artefactHandler.AddInSnake();
            }
        }
    }
}
