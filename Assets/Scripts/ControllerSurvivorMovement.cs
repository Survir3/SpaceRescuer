using System.Collections.Generic;
using UnityEngine;

public class ControllerSurvivorMovement : MonoBehaviour
{
    private PlayerMovement _player;
    private List<SurvivorMovement> _survivorMovement = new List<SurvivorMovement>();

    private void Awake()
    {
        _player= GetComponent<PlayerMovement>();
    }


    public void AddSurvivors(SurvivorMovement survivorMovement)
    {
        _survivorMovement.Add(survivorMovement);

        if (_survivorMovement.Count==1)
        {
            survivorMovement.SetTarget(_player.transform);
            Debug.Log("1___" + _survivorMovement.Count);
        }
        else
        {
            Transform target = _survivorMovement[_survivorMovement.Count-2].transform;
            survivorMovement.SetTarget(target);
            Debug.Log(">1___" + _survivorMovement.Count);
        }
    }
}
