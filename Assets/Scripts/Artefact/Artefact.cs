using System.Collections.Generic;
using UnityEngine;

public abstract class Artefact : Item
{
    [SerializeField] protected int _multiplier;

    public int Multiplier => _multiplier;

    public virtual void StartEffect(List<IMultiplied> multiplied)
    {

    }

    public virtual void StartEffect(IMultiplied multiplied)
    {

    }

    protected void DisableAfterEffect()
    {
        transform.parent.gameObject.SetActive(false);
    }
}