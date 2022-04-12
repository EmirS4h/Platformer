using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameManager gameManager;

    [Header("Speed Boost")]
    [SerializeField] float speedBoostAmount;
    [SerializeField] float speedBoostTime;
    private float playerStartSpeed;

    private void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Start()
    {
        playerStartSpeed = playerController.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameManager.objects.Add(gameObject);
            StartCoroutine(SpeedBooster(speedBoostAmount, speedBoostTime));
    }

    IEnumerator SpeedBooster(float boostAmount, float time)
    {
        playerController.moveSpeed *= boostAmount;
        yield return new WaitForSeconds(time);
        playerController.moveSpeed = playerStartSpeed;
        gameObject.SetActive(false);
    }
}
