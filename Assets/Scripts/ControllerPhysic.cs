using UnityEngine;

public class ControllerPhysic : MonoBehaviour
{
    public MovementPlayer movementPlayer;
    public ControllerSurvivorMovement ControllerSurvivorMovement;
    public ArtificialGravityAttractor artificialGravityAttractor;
    public Rigidbody _rigidbody;

    private void FixedUpdate()
    {
        artificialGravityAttractor.Attract(_rigidbody, transform);
        //movementPlayer.Move();
        //movementPlayer.Rotate();
        ControllerSurvivorMovement.Move();
        ControllerSurvivorMovement.Rotate();
    }
}
