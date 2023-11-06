using UnityEngine;

public class TimerArtefact : Artefact
{
    [SerializeField] private float _duration;

    public override void StartEffect(TimerToEndLevel timer)
    {
        timer.AddValue(_duration);
        StartCoroutine(DisableAfterEffect());
    }
}
