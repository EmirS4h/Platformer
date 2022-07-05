using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] Projectile bullet;
    [SerializeField] PlayerActions playerActions;

    [SerializeField] BulletPool pooler;
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
        bullet = pooler.GetPlayerBulletFromPool();
        if (bullet != null)
        {
            bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bullet.gameObject.transform.SetParent(null);
            bullet.gameObject.SetActive(true);
        }
    }
}
