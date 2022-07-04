using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] BulletPool _pool;
    [SerializeField] Projectile bullet;
    [SerializeField] PlayerActions playerActions;
    private void OnEnable()
    {
        playerActions.fireEvent += FireBullet;
    }
    private void OnDisable()
    {
        playerActions.fireEvent -= FireBullet;
    }
    private void FireBullet()
    {
        bullet = _pool.GetPlayerBulletFromPool();
        if (bullet != null)
        {
            bullet.gameObject.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bullet.gameObject.SetActive(true);
            bullet.Shoot();
        }
    }
}
