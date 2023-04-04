using UnityEngine;

public abstract class Artefact : Item
{
    [SerializeField] private int _multiplier;

    public int Multiplier => _multiplier;
}
