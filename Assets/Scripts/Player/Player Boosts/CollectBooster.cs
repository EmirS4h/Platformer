using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBooster : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public int boostType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (boostType == 1 && playerController.collectedDashBoosterAmount < 4)
            {
                playerController.collectedDashBoosterAmount++;
                playerController.SavePlayerData();
                if (playerController.collectedDashBoosterAmount==3)
                {
                    playerController.maxDash = 2;
                    playerController.SavePlayerData();
                }
                Destroy(gameObject);
            }
            else if (boostType == 2 && playerController.collectedJumpBoosterAmount < 4)
            {
                playerController.collectedJumpBoosterAmount++;
                playerController.SavePlayerData();
                if (playerController.collectedJumpBoosterAmount==3)
                {
                    playerController.hasDoubleJump = true;
                    playerController.SavePlayerData();
                }
                Destroy(gameObject);
            }
            else
            {
                return;
            }
        }
    }

}
