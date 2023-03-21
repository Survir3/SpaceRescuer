using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private List<Survivor> _survivors = new List<Survivor>();
    private List<Artefact> _artefacts= new List<Artefact>();

    public IReadOnlyList<Survivor> Survivors => _survivors;
    public int Points { get; private set; }

    public event UnityAction AddSurvivors;

    public void TakeSurvivor(Survivor survivor)
    {
        _survivors.Add(survivor);
        AddPoint(survivor.Points);
        AddSurvivors?.Invoke();
    }

    public void TakeArtefact(Artefact artefacts)
    {
        _artefacts.Add(artefacts);
        AddPoint(artefacts.Points);
    }

    private void AddPoint(int point)
    {
        if(point>0)
            Points += point;
    }
}