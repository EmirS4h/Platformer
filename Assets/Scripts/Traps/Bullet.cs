using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletParticlePool _particlePool;
    [SerializeField] BulletParticle _particle;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float force = 20.0f;
    [SerializeField] Color bulletColor;
    public enum BulletColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Purple,
    }
    public BulletColor bColor;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _particlePool = FindObjectOfType<BulletParticlePool>();

        switch (bColor)
        {
            case BulletColor.Red:
                bulletColor = Color.red;
                break;
            case BulletColor.Blue:
                bulletColor = Color.blue;
                break;
            case BulletColor.Green:
                bulletColor = Color.green;
                break;
            case BulletColor.Yellow:
                bulletColor = Color.yellow;
                break;
            case BulletColor.Purple:
                bulletColor = Color.magenta;
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _particle = _particlePool.GetFromPool();
        _particle.ChangeColor(bulletColor);
        _particle.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _particle.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Shoot()
    {
        rb.velocity = transform.right * force;
    }
    private void Start()
    {
        Shoot();
    }
}
