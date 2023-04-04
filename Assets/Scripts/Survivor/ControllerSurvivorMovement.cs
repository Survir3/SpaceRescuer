using System.Collections.Generic;
using UnityEngine;

public class ControllerSurvivorMovement : MonoBehaviour
{
    private MovementPlayer _player;
    private List<SurvivorMovement> _survivorMovements = new List<SurvivorMovement>();

    private void Awake()
    {
        _player = GetComponent<MovementPlayer>();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    public void AddSurvivor(SurvivorMovement survivor)
    {
        _survivorMovements.Add(survivor);
    }

    private void Move()
    {
        if(_survivorMovements.Count>0)
        {
            _survivorMovements[0].Move(_player._anchor);
        }

        if (_survivorMovements.Count > 1)
        {
            for (int i = 1; i < _survivorMovements.Count; i++)
            {
                Transform preveuPosition = _survivorMovements[i - 1]._anchor;
                _survivorMovements[i].Move(preveuPosition);
            }
        }
    }

    private void Rotate()
    {
        if (_survivorMovements.Count > 0)
        {
            _survivorMovements[0].Rotate(_player._anchor);
        }

        if (_survivorMovements.Count > 1)
        {

            for (int i = 1; i < _survivorMovements.Count; i++)
            {
                Transform preveuPosition = _survivorMovements[i - 1].transform;
                _survivorMovements[i].Rotate(preveuPosition);
            }
        }
    }
}
