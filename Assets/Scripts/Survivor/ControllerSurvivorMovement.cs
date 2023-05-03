using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class ControllerSurvivorMovement : MonoBehaviour
{
    private MovementPlayer _player;

    public readonly List<SurvivorMovement> SurvivorMovements = new List<SurvivorMovement>();

    private void Awake()
    {
        _player = GetComponent<MovementPlayer>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public void AddSurvivor(SurvivorMovement survivor)
    {
        SurvivorMovements.Add(survivor);

        if(SurvivorMovements.Count==1)
        {
            survivor.SetStart(_player.Anchor, _player.CurrentMultiplier);
        }
        else
        {
            survivor.SetStart(SurvivorMovements[SurvivorMovements.Count - 2].Anchor, _player.CurrentMultiplier);
        }
    }

    public void Move()
    {
        if(SurvivorMovements.Count>0)
         SurvivorMovements[0].MoveToTarget(_player.Rigidbody);

        for (int i = 1; i < SurvivorMovements.Count; i++)
        {
            Rigidbody preveuPosition = SurvivorMovements[i - 1].Rigidbody;
            SurvivorMovements[i].MoveToTarget(preveuPosition);
        }
    }

    public void Rotate()
    {
        if (SurvivorMovements.Count > 0)
            SurvivorMovements[0].RotateToTarget(_player.LookAt);

        for (int i = 1; i < SurvivorMovements.Count; i++)
        {
            Transform preveuPosition = SurvivorMovements[i - 1].LookAt;
            SurvivorMovements[i].RotateToTarget(preveuPosition);
        }
    }
}
