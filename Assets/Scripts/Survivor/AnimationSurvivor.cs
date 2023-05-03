using UnityEngine;

public class AnimationSurvivor : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] CollisionSurvivorHandler _survivorHandler;
    [SerializeField] GameObject _shield;

    private void OnEnable()
    {
        _survivorHandler.Added += StartAninationFollow;
        _shield.SetActive(false);
    }

    private void OnDisable()
    {
        _survivorHandler.Added -= StartAninationFollow;

    }
    private void StartAninationFollow(CollisionHandler collisionSurvivorHandler)
    {
        _shield.SetActive(true);
        _animator.SetTrigger(ConstantsAnimation.IsAdd);
    }
}
