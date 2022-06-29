using System.Collections;
using UnityEngine;
public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }
    public void Die()
    {
        PlayerController.Instance.spr.enabled = false;
        PlayerController.Instance.bxCollider.enabled = false;
        PlayerController.Instance.bloodParticle.Play();
        PlayerController.Instance.audioSource.Play();
        rb.bodyType = RigidbodyType2D.Static;
        StartCoroutine(RestartPlayer());
    }

    public IEnumerator RestartPlayer()
    {
        GameManager.Instance.ReActivateBack();
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.SentPlayerBackToStart();
        PlayerController.Instance.spr.enabled = true;
    }
}
