using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBooster : MonoBehaviour
{
    [SerializeField] Boost boost;
    [SerializeField] ParticleSystem ps;
    SpriteRenderer sp;
    CircleCollider2D cc;

    [SerializeField] bool playerOnBooster = false;

    [SerializeField] Sprite cardSprite;

    [SerializeField] GameObject card;
    [SerializeField] SpriteRenderer cardSpriteRenderer;

    AudioSource audioSource;

    [SerializeField] PlayerActions playerActions;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sp = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        card = transform.GetChild(1).gameObject;
        cardSpriteRenderer = card.GetComponent<SpriteRenderer>();
        cardSpriteRenderer.sprite = cardSprite;
    }

    private void OnEnable()
    {
        playerActions.interactEvent += Collect;
    }
    private void OnDisable()
    {
        playerActions.interactEvent -= Collect;
    }

    public void Collect()
    {
        if (playerOnBooster)
        {
            ApplyBooster();
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnBooster = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerOnBooster = true;
        if (playerOnBooster)
        {
            card.SetActive(true);
        }
        else
        {
            card.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnBooster = false;
        card.SetActive(false);
    }
    private IEnumerator PlayParticles()
    {
        sp.enabled = false;
        cc.enabled = false;
        ps.Play();
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
    private void ApplyBooster()
    {

        if (!PlayerController.Instance.hasCollectedPowerUpStone)
        {
            ShowCard();
            PlayerController.Instance.hasCollectedPowerUpStone = true;
        }

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

    private void ShowCard()
    {
        UiManager.Instance.ActivateItemCard();
    }
    private void HideCard()
    {
        UiManager.Instance.DeactivateItemCard();
    }

}
