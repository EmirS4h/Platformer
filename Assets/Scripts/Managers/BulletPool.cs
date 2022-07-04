using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] Bullet _prefab;
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] List<Bullet> _pool;
    [SerializeField] List<Projectile> _playerProjectilePool;

    [SerializeField] int _poolAmount;
    [SerializeField] bool normalBullet = true;
    private void Awake()
    {
        if (normalBullet)
        {
            for (int i = 0; i < _poolAmount; i++)
            {
                Bullet instance = Instantiate(_prefab);
                instance.gameObject.transform.SetParent(transform);
                instance.gameObject.SetActive(false);
                _pool.Add(instance);
            }
        }
        else
        {
            for (int i = 0; i < _poolAmount; i++)
            {
                Projectile instance = Instantiate(_projectilePrefab);
                instance.gameObject.SetActive(false);
                _playerProjectilePool.Add(instance);
            }
        }
    }
    public Bullet GetBulletFromPool()
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
    public Projectile GetPlayerBulletFromPool()
    {
        for (int i = 0; i < _playerProjectilePool.Count; i++)
        {
            if (!_playerProjectilePool[i].gameObject.activeInHierarchy)
            {
                return _playerProjectilePool[i];
            }
        }
        return null;
    }
}
