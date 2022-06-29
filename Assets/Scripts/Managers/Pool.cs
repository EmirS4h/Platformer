using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance;

    [SerializeField] GameObject _prefab;
    [SerializeField] List<GameObject> _pool;

    [SerializeField] int _poolAmount;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            for (int i = 0; i < _poolAmount; i++)
            {
                GameObject instance = Instantiate(_prefab);
                instance.transform.SetParent(transform);
                instance.SetActive(false);
                _pool.Add(instance);
            }
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
