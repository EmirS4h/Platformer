using System.Collections;
using UnityEngine;

public class CollectBooster : MonoBehaviour
{
    [SerializeField] Boost boost;
    [SerializeField] ParticleSystem ps;
    [SerializeField] SpriteRenderer sp;
    [SerializeField] CircleCollider2D cc;

    [SerializeField] bool playerOnBooster = false;

    [SerializeField] Sprite cardSprite;

    [SerializeField] GameObject card;
    [SerializeField] SpriteRenderer cardSpriteRenderer;

    [SerializeField] AudioSource audioSource;

    [SerializeField] PlayerActions playerActions;
    [SerializeField] NotifDetails notifDetails;

    private void Awake()
    {
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
            card.SetActive(true);
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

        switch (boost.type)
        {
            case Boost.Type.DoubleDashBooster:
                if (!PlayerPrefs.HasKey("hasCollectedDoubleDashBooster"))
                {
                    PlayerController.Instance.collectedDoubleDashBooster = true;
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                    PlayerController.Instance.maxDash = 2;
                    PlayerPrefs.SetInt("hasCollectedDoubleDashBooster", 1);
                    PlayerController.Instance.SavePlayerData();
                }
                break;
            case Boost.Type.DoubleJumpBooster:
                PlayerController.Instance.collectedDoubleJumpBooster = true;
                UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                PlayerController.Instance.hasDoubleJump = true;
                PlayerPrefs.SetInt("hasCollectedDoubleJumpBooster", 1);
                PlayerController.Instance.SavePlayerData();
                break;
            case Boost.Type.PermaDashForce:
                PlayerController.Instance.collectedDashForceBooster = true;
                UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                PlayerController.Instance.dashForce *= boost.permaDashForceBoostAmount;
                PlayerPrefs.SetInt("hasCollectedDashForceBooster", 1);
                PlayerController.Instance.SavePlayerData();
                break;
            case Boost.Type.PermaMoveSpeed:
                PlayerController.Instance.collectedMoveSpeedBooster = true;
                UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                PlayerController.Instance.moveSpeed *= boost.permaMoveSpeedBoostAmount;
                PlayerPrefs.SetInt("hasCollectedMoveSpeedBooster", 1);
                PlayerController.Instance.SavePlayerData();
                break;
            case Boost.Type.PermaJumpForce:
                PlayerController.Instance.collectedJumpForceBooster = true;
                UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                PlayerController.Instance.jumpForce *= boost.permaJumpForceBoostAmount;
                PlayerPrefs.SetInt("hasCollectedJumpForceBooster", 1);
                PlayerController.Instance.SavePlayerData();
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
