using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBooster : MonoBehaviour
{
    [SerializeField] Boost boost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (boost.type)
            {
                case Boost.Type.DoubleDashBooster:
                    if (PlayerController.Instance.collectedDoubleDashBoosterAmount < 3)
                    {
                        PlayerController.Instance.collectedDoubleDashBoosterAmount++;
                        if (PlayerController.Instance.collectedDoubleDashBoosterAmount==3)
                        {
                            PlayerController.Instance.maxDash = 2;
                        }
                        PlayerController.Instance.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                case Boost.Type.DoubleJumpBooster:
                    if (PlayerController.Instance.collectedDoubleJumpBoosterAmount < 3)
                    {
                        PlayerController.Instance.collectedDoubleJumpBoosterAmount++;
                        if (PlayerController.Instance.collectedDoubleJumpBoosterAmount==3)
                        {
                            PlayerController.Instance.hasDoubleJump = true;
                        }
                        PlayerController.Instance.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                case Boost.Type.PermaDashForce:
                    if (PlayerController.Instance.collectedDashForceBoosterAmount < 3)
                    {
                        PlayerController.Instance.collectedDashForceBoosterAmount++;
                        if (PlayerController.Instance.collectedDashForceBoosterAmount==3)
                        {
                            PlayerController.Instance.dashForce *= 1.5f;
                        }
                        PlayerController.Instance.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                case Boost.Type.PermaMoveSpeed:
                    if (PlayerController.Instance.collectedMoveSpeedBoosterAmount < 3)
                    {
                        PlayerController.Instance.collectedMoveSpeedBoosterAmount++;
                        if (PlayerController.Instance.collectedMoveSpeedBoosterAmount==3)
                        {
                            PlayerController.Instance.moveSpeed *= 1.5f;
                        }
                        PlayerController.Instance.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                case Boost.Type.PermaJumpForce:
                    if (PlayerController.Instance.collectedJumpForceBoosterAmount < 3)
                    {
                        PlayerController.Instance.collectedJumpForceBoosterAmount++;
                        if (PlayerController.Instance.collectedJumpForceBoosterAmount==3)
                        {
                            PlayerController.Instance.jumpForce *= 1.5f;
                        }
                        PlayerController.Instance.SavePlayerData();
                    }
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
