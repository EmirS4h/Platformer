using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem.MainModule ps;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>().main;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
    public void ChangeColor(Color color)
    {
        ps.startColor = color;
    }
}
