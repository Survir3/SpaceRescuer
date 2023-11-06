using UnityEngine;

[RequireComponent ((typeof(Survivor)), (typeof(SurvivorMovement)))]
public class CollisionSurvivorHandler : CollisionHandler
{
    private SurvivorMovement _survivorMovementTest;
    private Survivor _survivor;

    private void Awake()
    {
        _survivorMovementTest = GetComponent<SurvivorMovement>();
        _survivor = GetComponent<Survivor>();
    }

    public void GivePropertyTo(HandlerPathSnake handlerPathSnake, Points points)
    {
        handlerPathSnake.AddSurvivor(_survivorMovementTest);
        _survivor.GivePoints(points);
        AddInSnake();
    }
}
