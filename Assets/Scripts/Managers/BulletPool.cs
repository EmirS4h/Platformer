using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] Bullet _prefab;
    [SerializeField] List<Bullet> _pool;

    [SerializeField] int _poolAmount;
    private void Awake()
    {

        for (int i = 0; i < _poolAmount; i++)
        {
            Bullet instance = Instantiate(_prefab);
            instance.gameObject.transform.SetParent(transform);
            instance.gameObject.SetActive(false);
            _pool.Add(instance);
        }
    }
    public Bullet GetFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].gameObject.activeInHierarchy)
            {
                return _pool[i];
            }
        }
        return null;
    }
}
