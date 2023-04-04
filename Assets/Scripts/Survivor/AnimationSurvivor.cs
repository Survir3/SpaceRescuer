using UnityEngine;

public class AnimationSurvivor : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] CollisionSurvivorHandler _survivorHandler;
    [SerializeField] GameObject _shield;

    private void OnEnable()
    {
        _survivorHandler.HasAdd += StartAninationFollow;
        _shield.SetActive(false);
    }

    private void OnDisable()
    {
        _survivorHandler.HasAdd -= StartAninationFollow;

    }
    private void StartAninationFollow()
    {
        _animator.SetTrigger(ConstantsAnimation.IsAdd);
        _shield.SetActive(true);
    }
}
