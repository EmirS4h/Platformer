using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBooster : MonoBehaviour
{
    [SerializeField] Boost boost;
    [SerializeField] ParticleSystem ps;
    SpriteRenderer sp;
    CircleCollider2D cc;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }

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
                    break;
                case Boost.Type.PermaDashForce:
                    if (PlayerController.Instance.collectedDashForceBoosterAmount < 3)
                    {
                        PlayerController.Instance.collectedDashForceBoosterAmount++;
                        if (PlayerController.Instance.collectedDashForceBoosterAmount==3)
                        {
                            PlayerController.Instance.dashForce *= boost.permaDashForceBoostAmount;
                        }
                        PlayerController.Instance.SavePlayerData();
                    }
                    break;
                case Boost.Type.PermaMoveSpeed:
                    if (PlayerController.Instance.collectedMoveSpeedBoosterAmount < 3)
                    {
                        PlayerController.Instance.collectedMoveSpeedBoosterAmount++;
                        if (PlayerController.Instance.collectedMoveSpeedBoosterAmount==3)
                        {
                            PlayerController.Instance.moveSpeed *= boost.permaMoveSpeedBoostAmount;
                        }
                        PlayerController.Instance.SavePlayerData();
                    }
                    break;
                case Boost.Type.PermaJumpForce:
                    if (PlayerController.Instance.collectedJumpForceBoosterAmount < 3)
                    {
                        PlayerController.Instance.collectedJumpForceBoosterAmount++;
                        if (PlayerController.Instance.collectedJumpForceBoosterAmount==3)
                        {
                            PlayerController.Instance.jumpForce *= boost.permaJumpForceBoostAmount;
                        }
                        PlayerController.Instance.SavePlayerData();
                    }
                    break;
                default:
                    break;
            }
            StartCoroutine(PlayParticles());
        }
    }

    private IEnumerator PlayParticles()
    {
        sp.enabled = false;
        cc.enabled = false;
        ps.Play();
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
