using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBoost : MonoBehaviour
{
    [Header("Starting Forces")]
    [SerializeField] private float playerStartSpeed;
    [SerializeField] private float playerStartDashForce;
    [SerializeField] private float playerStartJumpForce;

    [SerializeField] private Boost boost;

    private void Start()
    {
        playerStartSpeed = PlayerController.Instance.moveSpeed;
        playerStartDashForce = PlayerController.Instance.dashForce;
        playerStartJumpForce = PlayerController.Instance.jumpForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !PlayerController.Instance.boostActive)
        {
            playerStartSpeed = PlayerController.Instance.moveSpeed;
            playerStartDashForce = PlayerController.Instance.dashForce;
            playerStartJumpForce = PlayerController.Instance.jumpForce;
            DeactivateBoostAndApply(boost.type, boost.boostAmount, boost.boostTime);
        }
    }

    IEnumerator Booster(Boost.Type boostType, float boostAmount, float time)
    {
        switch (boostType)
        {
            case Boost.Type.MoveSpeed:
            case Boost.Type.MoveSpeedFromChest:
                PlayerController.Instance.moveSpeed *= boostAmount;
                yield return new WaitForSeconds(time);
                PlayerController.Instance.moveSpeed = playerStartSpeed;
                PlayerController.Instance.boostActive = false;
                break;
            case Boost.Type.DashForce:
            case Boost.Type.DashForceFromChest:
                PlayerController.Instance.dashForce *= boostAmount;
                yield return new WaitForSeconds(time);
                PlayerController.Instance.dashForce = playerStartDashForce;
                PlayerController.Instance.boostActive = false;
                break;
            case Boost.Type.JumpForce:
            case Boost.Type.JumpForceFromChest:
                PlayerController.Instance.jumpForce *= boostAmount;
                yield return new WaitForSeconds(time);
                PlayerController.Instance.jumpForce = playerStartJumpForce;
                PlayerController.Instance.boostActive = false;
                break;
        }
    }

    private void DeactivateBoostAndApply(Boost.Type boostType, float amount, float time)
    {
        PlayerController.Instance.boostActive = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (boost.type == Boost.Type.MoveSpeedFromChest || boost.type == Boost.Type.DashForceFromChest || boost.type == Boost.Type.JumpForceFromChest)
        {
            GameManager.Instance.boostObjectFromChest.Add(gameObject);
            StartCoroutine(Booster(boostType, amount, time));
        }
        else
        {
            GameManager.Instance.boostObject.Add(gameObject);
            StartCoroutine(Booster(boostType, amount, time));
        }
    }
}
