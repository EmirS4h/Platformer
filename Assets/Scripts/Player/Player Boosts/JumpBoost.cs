using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameManager gameManager;

    [Header("Jump Boost")]
    [SerializeField] float jumpBoostAmount;
    [SerializeField] float jumpBoostTime;
    private float playerStartJumpForce;

    private void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Start()
    {
        playerStartJumpForce = playerController.jumpForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameManager.objects.Add(gameObject);
            StartCoroutine(JumpBooster(jumpBoostAmount, jumpBoostTime));
    }

    IEnumerator JumpBooster(float boostAmount, float time)
    {
        playerController.jumpForce *= boostAmount;
        yield return new WaitForSeconds(time);
        playerController.jumpForce = playerStartJumpForce;
        gameObject.SetActive(false);
    }
}
