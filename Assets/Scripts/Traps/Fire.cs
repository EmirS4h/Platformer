using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletPos;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound;

    [SerializeField] float time = 1.0f;
    [SerializeField] float repeatRate = 1.0f;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
    public Direction direction;

    private void Start()
    {
        InvokeRepeating(nameof(FireBullet), time, repeatRate);
    }

    private void FireBullet()
    {
        bullet = BulletPool.Instance.GetFromPool();
        bullet.gameObject.transform.SetPositionAndRotation(bulletPos.position, bulletPos.rotation);
        bullet.gameObject.SetActive(true);

        switch (direction)
        {
            case Direction.Up:
                bullet.Shoot(Vector2.up);
                break;
            case Direction.Down:
                bullet.Shoot(Vector2.down);
                break;
            case Direction.Left:
                bullet.Shoot(Vector2.left);
                break;
            case Direction.Right:
                bullet.Shoot(Vector2.right);
                break;
            default:
                break;
        }
        PlayFireSound();
    }
    private void PlayFireSound()
    {
        audioSource.PlayOneShot(sound);
    }

}
