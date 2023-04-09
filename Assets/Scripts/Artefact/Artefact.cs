using UnityEngine;

public abstract class Artefact : Item
{
    [SerializeField] protected int _multiplier;

    public int Multiplier => _multiplier;

    public abstract void StartEffect(IMultiplied multiplied);
}
