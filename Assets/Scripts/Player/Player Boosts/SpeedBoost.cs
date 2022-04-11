using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [Header("Speed Boost")]
    [SerializeField] float speedBoostAmount;
    [SerializeField] float speedBoostTime;
    private float playerStartSpeed;

    private void Start()
    {
        playerStartSpeed = playerController.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(SpeedBooster(speedBoostAmount, speedBoostTime));
    }

    IEnumerator SpeedBooster(float boostAmount, float time)
    {
        playerController.moveSpeed *= boostAmount;
        yield return new WaitForSeconds(time);
        playerController.moveSpeed = playerStartSpeed;
        Destroy(gameObject);
    }
}
