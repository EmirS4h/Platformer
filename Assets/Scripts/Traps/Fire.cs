using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] BulletPool _pool;
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletPos;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound;

    [SerializeField] float time = 1.0f;
    [SerializeField] float repeatRate = 1.0f;

    [SerializeField] bool burst = false;
    [SerializeField] int burstAmount = 3;
    [SerializeField] float timeBetweenBurst = 3.0f;

    WaitForSeconds delay;
    private void Awake()
    {
        delay = new WaitForSeconds(0.2f);
    }
    private void Start()
    {
        if (burst)
        {
            InvokeRepeating(nameof(BurstFire), time, timeBetweenBurst);
        }
        else
        {
            InvokeRepeating(nameof(FireBullet), time, repeatRate);
        }
    }

    private void FireBullet()
    {
        bullet = _pool.GetBulletFromPool();
        if (bullet != null)
        {
            bullet.gameObject.transform.SetPositionAndRotation(bulletPos.position, bulletPos.rotation);
            bullet.gameObject.SetActive(true);
            bullet.Shoot();
            PlayFireSound();
        }
    }
    private void PlayFireSound()
    {
        audioSource.PlayOneShot(sound);
    }
    private void BurstFire()
    {
        StartCoroutine(DelayFire());
    }
    private IEnumerator DelayFire()
    {
        for (int i = 0; i < burstAmount; i++)
        {
            FireBullet();
            yield return delay;
        }
    }
}
