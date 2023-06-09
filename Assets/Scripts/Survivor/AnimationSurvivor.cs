using UnityEngine;

public class AnimationSurvivor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CollisionSurvivorHandler _survivorHandler;
    [SerializeField] private GameObject _shield;

    private void OnEnable()
    {
        _survivorHandler.Added += StartAninationFollow;
        _shield.SetActive(false);
    }

    private void OnDisable()
    {
        _survivorHandler.Added -= StartAninationFollow;
    }
    private void StartAninationFollow(CollisionHandler collisionHandler)
    {
        _shield.SetActive(true);
        _animator.SetTrigger(ConstantsString.AnimanionIsAdd);
    }
}
