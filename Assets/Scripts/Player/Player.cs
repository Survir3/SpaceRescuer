using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Points _points;

    private List<Survivor> _survivors = new List<Survivor>();
    private List<Artefact> _artefacts= new List<Artefact>();

    public Points Points => _points;
    public IReadOnlyList<Survivor> Survivors => _survivors;
    public IReadOnlyList<Artefact> Artefacts=> _artefacts;
   
    public event UnityAction AddSurvivor;
    public event UnityAction AddArtefact;

    public void TakeSurvivor(Survivor survivor)
    {
        _survivors.Add(survivor);
        Points.Add(survivor.Points);
        AddSurvivor?.Invoke();
    }

    public void TakeArtefact(Artefact artefacts)
    {
        _artefacts.Add(artefacts);
        Points.Add(artefacts.Points);
        AddArtefact?.Invoke();
    }

    private void AddPoint(int point)
    {
   //     if(point>0)
          //  Points += point;
    }

    private void StartEffect()
    {

    }

    public void Dead()
    {
        Debug.Log("dead");
    }
}