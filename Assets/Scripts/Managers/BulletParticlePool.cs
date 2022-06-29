using System.Collections.Generic;
using UnityEngine;

public class BulletParticlePool : MonoBehaviour
{
    [SerializeField] BulletParticle _prefab;
    [SerializeField] List<BulletParticle> _pool;

    [SerializeField] int _poolAmount;
    private void Awake()
    {

        for (int i = 0; i < _poolAmount; i++)
        {
            BulletParticle instance = Instantiate(_prefab);
            instance.gameObject.transform.SetParent(transform);
            instance.gameObject.SetActive(false);
            _pool.Add(instance);
        }
    }
    public BulletParticle GetFromPool()
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
