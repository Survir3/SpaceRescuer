using System.Collections;
using UnityEngine;
public class SpeedMovementArtefact : Artefact
{
    [SerializeField] private float _duration;
        
    public override void StartEffect(IMultiplied multiplied)
    {
        StartCoroutine(Timer(multiplied));
    }

    private IEnumerator Timer(IMultiplied multiplied)
    {
        multiplied.SetMultiplier(_multiplier);
        yield return new WaitForSeconds(_duration);
        multiplied.SetDefaultMultiplier();
    }
}
