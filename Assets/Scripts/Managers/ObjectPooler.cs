using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] List<GameObject> _pool;
    [SerializeField] int _count = 10;
    private void Awake()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject instance = Instantiate(_prefab);
            instance.transform.SetParent(transform);
            instance.SetActive(false);
            _pool.Add(instance);
        }
    }
    public GameObject GetFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        return null;
    }
}
