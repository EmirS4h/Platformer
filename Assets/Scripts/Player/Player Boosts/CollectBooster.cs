using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBooster : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    enum BoosterType
    {
        MaxDash,
        DoubleJump,
        DashForce,
    }
    [SerializeField] BoosterType boosterType;
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (boosterType == BoosterType.MaxDash && playerController.collectedDashBoosterAmount < 3)
            {
                playerController.collectedDashBoosterAmount++;
                if (playerController.collectedDashBoosterAmount==3)
                {
                    playerController.maxDash = 2;
                }
                playerController.SavePlayerData();
                Destroy(gameObject);
            }
            else if (boosterType == BoosterType.DoubleJump && playerController.collectedJumpBoosterAmount < 3)
            {
                playerController.collectedJumpBoosterAmount++;
                if (playerController.collectedJumpBoosterAmount==3)
                {
                    playerController.hasDoubleJump = true;
                }
                playerController.SavePlayerData();
                Destroy(gameObject);
            }
            else if (boosterType == BoosterType.DashForce && playerController.collectedDashForceBoosterAmount < 3)
            {
                playerController.collectedDashForceBoosterAmount++;
                if (playerController.collectedDashForceBoosterAmount==3)
                {
                    playerController.dashForce *= 1.5f;
                }
                playerController.SavePlayerData();
                Destroy(gameObject);
            }
            else
            {
                return;
            }
        }
    }

}
