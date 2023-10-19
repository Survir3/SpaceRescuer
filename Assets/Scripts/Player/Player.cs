using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Points))]
public class Player : MonoBehaviour, INeededSwitchPlayMode
{ 
   [SerializeField] private Points _points;

    public bool IsDead { get; private set; } = false;
    public Points Points => _points;

    public bool IsPause { get; set; }

    public event UnityAction IsDie;
    public event UnityAction NeededPause;
    public event UnityAction NeededPlay;

    public void Dead()
    {
        IsDead=true;
        RequestPause();
        IsDie.Invoke();
    }

    public void RequestPlay()
    {
        IsPause = false;
        NeededPlay.Invoke();
    }

    public void RequestPause()
    {
        IsPause = true;
        NeededPause.Invoke();
    }
}