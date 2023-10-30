using UnityEngine;

[RequireComponent((typeof(Player)), (typeof(MovementPlayer)), typeof(SetterTargetSurvivorMovement))]
public class CollisionPlayerHandler : MonoBehaviour
{
    private Player _player;
    private SetterTargetSurvivorMovement _controllerSurvivorMovement;
    private HandlerArtefactEffect _controllerArtefactEffect;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _controllerSurvivorMovement = GetComponent<SetterTargetSurvivorMovement>();
        _controllerArtefactEffect = GetComponent<HandlerArtefactEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TryGetComponent<CollisionHandler>(out CollisionHandler handler);

        switch (handler)
        {
            case CollisionArtefactHandler artefactHandler:
                TryAddInSnake(artefactHandler);
                break;
            case CollisionSurvivorHandler survivorHandler:
                TryAddInSnake(survivorHandler);
                break;
            case CollisionEnemyHandler enemyHandler:
                _player.Dead();
                break;
            default:
                break;
        }
    }

    private bool TryAddInSnake(CollisionSurvivorHandler survivorHandler)
    {
        if (survivorHandler.IsAdded)
        {
            _player.Dead();
            return !survivorHandler.IsAdded;
        }
        else
        {
            survivorHandler.GetComponent<Survivor>().GivePoints(_player.Points);
            _controllerSurvivorMovement.AddSurvivor(survivorHandler.GetComponent<SurvivorMovement>());
            survivorHandler.AddInSnake();
            return survivorHandler.IsAdded;
        }
    }

    private bool TryAddInSnake(CollisionArtefactHandler artefactHandler)
    {
        if (artefactHandler.IsAdded)
        {
            return !artefactHandler.IsAdded;
        }
        else
        {
            Artefact artefact = artefactHandler.GetComponentInParent<Artefact>();
            artefact.GivePoints(_player.Points);
            _controllerArtefactEffect.GetEffect(artefact);
            artefactHandler.AddInSnake();
            return artefactHandler.IsAdded;
        }
    }

}
