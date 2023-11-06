using UnityEngine;

[RequireComponent((typeof(Player)), (typeof(MovementPlayer)), typeof(HandlerPathSnake))]
public class CollisionPlayerHandler : MonoBehaviour
{
    private Player _player;
    private HandlerPathSnake _handlerPathSnake;
    private HandlerArtefactEffect _handlerArtefactEffect;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _handlerPathSnake = GetComponent<HandlerPathSnake>();
        _handlerArtefactEffect = GetComponent<HandlerArtefactEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<CollisionHandler>(out CollisionHandler handler);

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
            survivorHandler.GivePropertyTo(_handlerPathSnake, _player.Points);
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
            artefactHandler.GivePropertyTo(_handlerArtefactEffect, _player.Points);
            return artefactHandler.IsAdded;
        }
    }
}
