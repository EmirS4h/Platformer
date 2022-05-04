using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBoost : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [Header("Starting Forces")]
    [SerializeField] private float playerStartSpeed;
    [SerializeField] private float playerStartDashForce;
    [SerializeField] private float playerStartJumpForce;

    [SerializeField] private Boost boost;

    private void Start()
    {
        playerStartSpeed = playerController.moveSpeed;
        playerStartDashForce = playerController.dashForce;
        playerStartJumpForce = playerController.jumpForce;

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeactivateBoostAndApply(boost.type, boost.boostAmount, boost.boostTime);
        }
    }

    IEnumerator Booster(Boost.Type boostType, float boostAmount, float time)
    {
        switch (boostType)
        {
            case Boost.Type.MoveSpeed:
                playerController.moveSpeed *= boostAmount;
                yield return new WaitForSeconds(time);
                playerController.moveSpeed = playerStartSpeed;
                gameObject.SetActive(false);
                break;
            case Boost.Type.DashForce:
                playerController.dashForce *= boostAmount;
                yield return new WaitForSeconds(time);
                playerController.dashForce = playerStartDashForce;
                gameObject.SetActive(false);
                break;
            case Boost.Type.JumpForce:
                playerController.jumpForce *= boostAmount;
                yield return new WaitForSeconds(time);
                playerController.jumpForce = playerStartJumpForce;
                gameObject.SetActive(false);
                break;
        }
    }

    private void DeactivateBoostAndApply(Boost.Type boostType, float amount, float time)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.boostObject.Add(gameObject);
        StartCoroutine(Booster(boostType, amount, time));
    }
}
