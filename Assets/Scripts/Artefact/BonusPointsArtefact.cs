using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPointsArtefact : Artefact
{
    public override void StartEffect(IMultiplied multiplied)
    {
        multiplied.SetMultiplier(_multiplier);

        DisableAfterEffect();
    }
}
