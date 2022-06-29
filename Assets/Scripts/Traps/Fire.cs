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
    private void Start()
    {
        InvokeRepeating(nameof(FireBullet), time, repeatRate);
    }

    private void FireBullet()
    {
        bullet = _pool.GetFromPool();
        bullet.gameObject.transform.SetPositionAndRotation(bulletPos.position, bulletPos.rotation);
        bullet.gameObject.SetActive(true);
        bullet.Shoot();
        PlayFireSound();
    }
    private void PlayFireSound()
    {
        audioSource.PlayOneShot(sound);
    }

}
