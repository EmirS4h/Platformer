using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
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
        //Instantiate(bullet, bulletPos.position, bulletPos.transform.rotation);
        GameObject bullet = Pool.Instance.GetFromPool();
        bullet.transform.SetPositionAndRotation(bulletPos.position, bulletPos.rotation);
        bullet.SetActive(true);
        PlayFireSound();
    }
    private void PlayFireSound()
    {
        audioSource.PlayOneShot(sound);
    }
}
