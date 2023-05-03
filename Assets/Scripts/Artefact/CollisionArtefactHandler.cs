using UnityEngine;

public class CollisionArtefactHandler : CollisionHandler
{
    [SerializeField] private Artefact _artefact;
    [SerializeField] private Animation _animation;
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Player>() != null)
        {
            _rigidbody.isKinematic = true;
            _collider.isTrigger = true;
            _animation.Play();
        }
    }
}
