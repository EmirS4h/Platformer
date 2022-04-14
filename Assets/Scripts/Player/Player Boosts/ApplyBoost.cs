using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBoost : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameManager gameManager;

    [Header("Speed Boost")]
    [SerializeField] float speedBoostAmount;
    [SerializeField] float speedBoostTime;

    [Header("Dash Boost")]
    [SerializeField] float dashBoostAmount;
    [SerializeField] float dashBoostTime;

    [Header("Jump Boost")]
    [SerializeField] float jumpBoostAmount;
    [SerializeField] float jumpBoostTime;

    [Header("Starting Forces")]
    private float playerStartSpeed;
    private float playerStartDashForce;
    private float playerStartJumpForce;

    private void Start()
    {
        playerStartSpeed = playerController.moveSpeed;
        playerStartDashForce = playerController.dashForce;
        playerStartJumpForce = playerController.jumpForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpeedBoost"))
        {
            DeactivateBoostAndApply(collision.gameObject, 0, speedBoostAmount, speedBoostTime);
        }
        else if (collision.gameObject.CompareTag("DashBoost"))
        {
            DeactivateBoostAndApply(collision.gameObject, 1, dashBoostAmount, dashBoostTime);
        }
        else if (collision.gameObject.CompareTag("JumpBoost"))
        {
            DeactivateBoostAndApply(collision.gameObject, 2, jumpBoostAmount, jumpBoostTime);
        }
    }

    IEnumerator Booster(GameObject boost, int boostType, float boostAmount, float time)
    {
        switch (boostType)
        {
            case 0:
                playerController.moveSpeed *= boostAmount;
                yield return new WaitForSeconds(time);
                playerController.moveSpeed = playerStartSpeed;
                boost.SetActive(false);
                break;
            case 1:
                playerController.dashForce *= boostAmount;
                yield return new WaitForSeconds(time);
                playerController.dashForce = playerStartDashForce;
                boost.SetActive(false);
                break;
            case 2:
                playerController.jumpForce *= boostAmount;
                yield return new WaitForSeconds(time);
                playerController.jumpForce = playerStartJumpForce;
                boost.SetActive(false);
                break;
        }
    }

    private void DeactivateBoostAndApply(GameObject boost, int boostType, float amount, float time)
    {
        boost.GetComponent<SpriteRenderer>().enabled = false;
        boost.GetComponent<BoxCollider2D>().enabled = false;
        gameManager.boostObject.Add(boost);
        StartCoroutine(Booster(boost, boostType, amount, time));
    }
}
