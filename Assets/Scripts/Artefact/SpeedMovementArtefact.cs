using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpeedMovementArtefact : Artefact
{
    [SerializeField] private float _duration;
        
    public override void StartEffect(List<IMultiplied> multiplied)
    {
        StartCoroutine(Effect(multiplied));
    }

    private IEnumerator Effect(List<IMultiplied> multiplied)
    {
        foreach (var item in multiplied)
        {
            item.SetMultiplier(_multiplier);
        }

        yield return new WaitForSeconds(_duration);    

        foreach (var item in multiplied)
        {
            item.SetDefaultMultiplier();
        }

        StartCoroutine(DisableAfterEffect());
    }
}
