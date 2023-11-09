using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(ArtificialGravityAttractor))]
public class Pool : MonoBehaviour
{
    [SerializeField] protected int _count;    
    [SerializeField] private Transform _container;
    [SerializeField] private ArtificialGravityAttractor _artificialGravityAttractor;

    private List<GameObject> _pool = new List<GameObject>(); 

    public int Count => _count;

    protected void Init(List<GameObject> prefabs)
    {
        for (int i = 0; i < Count; i++)
        {
            int randomOrder = Random.Range(0, prefabs.Count);
            GameObject newObject = Instantiate(prefabs[randomOrder], _container);
            _pool.Add(newObject);
            newObject.GetComponent<ArtificialGravityBody>().Init(_artificialGravityAttractor);
            newObject.SetActive(false);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.transform.parent == _container);
        return result != null;
    }
}
