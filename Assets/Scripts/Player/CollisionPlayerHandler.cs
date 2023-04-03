using UnityEngine;

[RequireComponent ((typeof(Player)), (typeof(MovementPlayer)), typeof(ControllerSurvivorMovement))]
public class CollisionPlayerHandler : MonoBehaviour
{
    private Player _player;
    private ControllerSurvivorMovement _controllerSurvivorMovement;
    
    private void Awake()
    {
        _player= GetComponent<Player>();
        _controllerSurvivorMovement= GetComponent<ControllerSurvivorMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CollisionSurvivorHandler>(out CollisionSurvivorHandler survivor))
        {
            if (survivor.IsAdded)
            {
                return;
            }
            else
            {
            survivor.AddInSnake();
            _controllerSurvivorMovement.AddSurvivor(survivor.GetComponent<SurvivorMovement>());
            TryAddToPlayer(other.gameObject);
            }
        }
        else if(other.gameObject.TryGetComponent<CollisionArtefactHandler>(out CollisionArtefactHandler artefact))
        {
            if (artefact.IsAdded)
            {
                return;
            }
            else
            {
                artefact.AddInSnake();
                TryAddToPlayer(other.gameObject);
            }
        }
    }

    private void TryAddToPlayer(GameObject adderPlayer)
     {
        adderPlayer.TryGetComponent<Item>(out Item item);

        switch (item)
        {     
            case Survivor survivor:
                _player.TakeSurvivor(survivor);
                break;
            case Artefact artefact:
                _player.TakeArtefact(artefact);
                break;
            default: 
                break;
        }
    }
}
