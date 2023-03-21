using UnityEngine;

[RequireComponent ((typeof(Player)), (typeof(PlayerMovement)), typeof(ControllerSurvivorMovement))]
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
        if (other.gameObject.TryGetComponent<CollisionSurvivorHandler>(out CollisionSurvivorHandler item))
        {
            if (item.IsAdded)
                return;

            item.IsAdded=true;
            TryAddToPlayer(other.gameObject);
            TryAddToSurvivorMovement(other.gameObject);
        }
    }

    public void TryAddToSurvivorMovement(GameObject adderPlayer)
    {
        if (adderPlayer.TryGetComponent<SurvivorMovement>(out SurvivorMovement survivorMovement))
        {
            _controllerSurvivorMovement.AddSurvivors(survivorMovement);
        }
    }

    public void TryAddToPlayer(GameObject adderPlayer)
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
