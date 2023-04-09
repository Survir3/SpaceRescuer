using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionArtefactHandler : CollisionHandler
{
    [SerializeField] private Artefact _artefact;
    [SerializeField] private Animation _animation;

    private void OnTriggerEnter(Collider other)
    {
        _animation.Play();
    }
}
