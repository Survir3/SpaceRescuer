using UnityEngine;

[RequireComponent (typeof(IAdder))]
public class CollisionSurvivorHandler : MonoBehaviour
{
    private IAdder _adderPlayer;
    public bool IsAdded = false;

    private void Awake()
    {
        _adderPlayer = GetComponent<IAdder>();
    }
}
