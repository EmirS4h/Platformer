using System.Collections;
using TMPro;
using UnityEngine;

public class ApplyBoost : MonoBehaviour
{
    public enum Boost
    {
        JumpForce,
        DashForce,
        MoveSpeed,
        JumpForceFromChest,
        DashForceFromChest,
        MoveSpeedFromChest,
    }
    public Boost type;
    public float boostAmount;
    public float boostTime;

    [Header("Starting Forces")]
    [SerializeField] private float playerStartSpeed;
    [SerializeField] private float playerStartDashForce;
    [SerializeField] private float playerStartJumpForce;

    //[SerializeField] private Boost boost;
    [SerializeField] private bool playerOnBooster = false;

    [SerializeField] GameObject card;
    [SerializeField] PlayerActions playerActions;

    [SerializeField] TextMeshPro stats;

    [SerializeField] ParticleSystem ps;
    [SerializeField] bool randomBoostStats = true;
    [Header("Sprite")]
    [SerializeField] Sprite sprite;
    private void Awake()
    {
        if (randomBoostStats)
        {
            boostAmount = Random.Range(1.1f, 1.5f);
            boostTime = Random.Range(3.0f, 10.0f);
        }
        stats.SetText("+"+ Mathf.Floor((boostAmount-Mathf.Floor(boostAmount))*100) + "% " + type + " For " + Mathf.Floor(boostTime) + " Seconds");
    }
    private void OnEnable()
    {
        playerActions.interactEvent += Apply;
    }
    private void OnDisable()
    {
        playerActions.interactEvent -= Apply;
    }
    private void Start()
    {
        playerStartSpeed = PlayerController.Instance.moveSpeed;
        playerStartDashForce = PlayerController.Instance.dashForce;
        playerStartJumpForce = PlayerController.Instance.jumpForce;
    }

    public void Apply()
    {
        if (playerOnBooster && !PlayerController.Instance.boostActive)
        {
            ps.Play();
            DeactivateBoostAndApply(type, boostAmount, boostTime);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerOnBooster = true;
        if (playerOnBooster && !PlayerController.Instance.boostActive)
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
        card.SetActive(false);
        playerOnBooster = false;
    }

    IEnumerator Booster(Boost boostType, float boostAmount, float time)
    {
        switch (boostType)
        {
            case Boost.MoveSpeed:
            case Boost.MoveSpeedFromChest:
                PlayerController.Instance.moveSpeed *= boostAmount;
                UiManager.Instance.ActivatePotImage(sprite);
                yield return new WaitForSeconds(time);
                UiManager.Instance.DeactivatePotImage();
                PlayerController.Instance.moveSpeed = playerStartSpeed;
                PlayerController.Instance.boostActive = false;
                break;
            case Boost.DashForce:
            case Boost.DashForceFromChest:
                PlayerController.Instance.dashForce *= boostAmount;
                UiManager.Instance.ActivatePotImage(sprite);
                yield return new WaitForSeconds(time);
                UiManager.Instance.DeactivatePotImage();
                PlayerController.Instance.dashForce = playerStartDashForce;
                PlayerController.Instance.boostActive = false;
                break;
            case Boost.JumpForce:
            case Boost.JumpForceFromChest:
                PlayerController.Instance.jumpForce *= boostAmount;
                UiManager.Instance.ActivatePotImage(sprite);
                yield return new WaitForSeconds(time);
                UiManager.Instance.DeactivatePotImage();
                PlayerController.Instance.jumpForce = playerStartJumpForce;
                PlayerController.Instance.boostActive = false;
                break;
        }
    }

    private void DeactivateBoostAndApply(Boost boostType, float amount, float time)
    {
        PlayerController.Instance.boostActive = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (type == Boost.MoveSpeedFromChest || type == Boost.DashForceFromChest || type == Boost.JumpForceFromChest)
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
