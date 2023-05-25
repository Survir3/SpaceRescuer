using UnityEngine;

public abstract class Item : MonoBehaviour, IGiverPoints, IAdder
{
    [SerializeField] private int _points;

    public int Points => _points;

    public void GivePoints(Points player)
    {
        player.Add(_points);
    }
}
