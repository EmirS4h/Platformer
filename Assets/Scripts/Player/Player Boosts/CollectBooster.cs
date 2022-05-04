using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBooster : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] Boost boost;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (boost.type)
            {
                case Boost.Type.DoubleDashBooster:
                    if (playerController.collectedDoubleDashBoosterAmount < 3)
                    {
                        playerController.collectedDoubleDashBoosterAmount++;
                        if (playerController.collectedDoubleDashBoosterAmount==3)
                        {
                            playerController.maxDash = 2;
                        }
                        playerController.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                case Boost.Type.DoubleJumpBooster:
                    if (playerController.collectedDoubleJumpBoosterAmount < 3)
                    {
                        playerController.collectedDoubleJumpBoosterAmount++;
                        if (playerController.collectedDoubleJumpBoosterAmount==3)
                        {
                            playerController.hasDoubleJump = true;
                        }
                        playerController.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                case Boost.Type.PermaDashForce:
                    if (playerController.collectedDashForceBoosterAmount < 3)
                    {
                        playerController.collectedDashForceBoosterAmount++;
                        if (playerController.collectedDashForceBoosterAmount==3)
                        {
                            playerController.dashForce *= 1.5f;
                        }
                        playerController.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                case Boost.Type.PermaMoveSpeed:
                    if (playerController.collectedMoveSpeedBoosterAmount < 3)
                    {
                        playerController.collectedMoveSpeedBoosterAmount++;
                        if (playerController.collectedMoveSpeedBoosterAmount==3)
                        {
                            playerController.moveSpeed *= 1.5f;
                        }
                        playerController.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                case Boost.Type.PermaJumpForce:
                    if (playerController.collectedJumpForceBoosterAmount < 3)
                    {
                        playerController.collectedJumpForceBoosterAmount++;
                        if (playerController.collectedJumpForceBoosterAmount==3)
                        {
                            playerController.jumpForce *= 1.5f;
                        }
                        playerController.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
