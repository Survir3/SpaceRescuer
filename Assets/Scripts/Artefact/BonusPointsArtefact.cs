public class BonusPointsArtefact : Artefact
{
    public override void StartEffect(IMultiplied multiplied)
    {
        multiplied.SetMultiplier(_multiplier);
        StartCoroutine(DisableAfterEffect());
    }
}
