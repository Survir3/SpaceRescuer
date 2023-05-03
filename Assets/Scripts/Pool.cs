using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _count;

    private List<GameObject> _pool= new List<GameObject>();

    protected void Init(GameObject prefab)
    {
        for (int i = 0; i < _count; i++)
        {
        GameObject newObject = Instantiate(prefab, _container);
        _pool.Add( newObject );
        newObject.SetActive(false);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result=_pool.First(p => p.activeSelf == false);
        return result != null;
    }
}
