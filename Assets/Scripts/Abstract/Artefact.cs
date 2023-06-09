using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Artefact : Item
{
    [SerializeField] protected int _multiplier;

    private float _delayDisable=2;
    public int Multiplier => _multiplier;

    public virtual void StartEffect(List<IMultiplied> multiplied)
    {

    }

    public virtual void StartEffect(IMultiplied multiplied)
    {

    }

    protected IEnumerator DisableAfterEffect()
    {
        yield return new WaitForSeconds(_delayDisable);
        transform.parent.gameObject.SetActive(false);
    }
}