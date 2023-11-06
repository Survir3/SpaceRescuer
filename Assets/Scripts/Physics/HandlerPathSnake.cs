using System.Collections.Generic;
using UnityEngine;

public class HandlerPathSnake : MonoBehaviour
{
    public static int MaxCountLastPoints = 30;

    [SerializeField] private MovementPlayer _movementPlayer;

    private List<Movement> _movementsSnake = new List<Movement>();
    private List<Queue<Vector3>> _pathsSurvivors = new List<Queue<Vector3>>();

    public IReadOnlyList<Movement> MovementsSnake => _movementsSnake;

    private void Awake()
    {
        AddMovementToPoints(_movementPlayer);
    }

    private void FixedUpdate()
    {
        SavePathHead();
        MovePath();
        DeletePathPassed();
    }

    public void AddSurvivor(SurvivorMovement survivor)
    {
        int lastPath = _pathsSurvivors.Count - 1;

        survivor.SetStart(_pathsSurvivors[lastPath].Dequeue(), _movementPlayer.CurrentMultiplier);
        AddMovementToPoints(survivor);
    }

    private void SavePathHead()
    {
        _pathsSurvivors[0].Enqueue(transform.position);
    }

    private void DeletePathPassed()
    {
        int lastPath = _pathsSurvivors.Count - 1;

        if (_pathsSurvivors[lastPath].Count > HandlerPathSnake.MaxCountLastPoints)
            _pathsSurvivors[lastPath].Dequeue();
    }

    private void MovePath()
    {
        for (int i = 1; i < _pathsSurvivors.Count; i++)
        {
            Vector3 targetPoint = _pathsSurvivors[i - 1].Dequeue();
            _movementsSnake[i].Move(targetPoint);
            _pathsSurvivors[i].Enqueue(targetPoint);
        }
    }

    private void AddMovementToPoints(Movement movement)
    {
        _movementsSnake.Add(movement);
        _pathsSurvivors.Add(new Queue<Vector3>());
    }
}
