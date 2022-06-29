using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletParticlePool _pool;
    [SerializeField] BulletParticle _particle;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float force = 20.0f;

    public enum BulletColor
    {
        Red,
        Blue,
        Green,
        Yellow,
    }
    public BulletColor bColor;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _pool = FindObjectOfType<BulletParticlePool>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _particle = _pool.GetFromPool();
        switch (bColor)
        {
            case BulletColor.Red:
                _particle.ChangeColor(Color.red);
                break;
            case BulletColor.Blue:
                _particle.ChangeColor(Color.blue);
                break;
            case BulletColor.Green:
                _particle.ChangeColor(Color.green);
                break;
            case BulletColor.Yellow:
                _particle.ChangeColor(Color.yellow);
                break;
            default:
                break;
        }
        _particle.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _particle.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Shoot()
    {
        rb.velocity = transform.right * force;
    }
}
