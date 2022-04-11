using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoost : MonoBehaviour
{

    [SerializeField] PlayerController playerController;

    [Header("Dash Boost")]
    [SerializeField] float dashBoostAmount;
    [SerializeField] float dashBoostTime;
    private float playerStartDashForce;

    private void Start()
    {
        playerStartDashForce = playerController.dashForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DashBooster(dashBoostAmount, dashBoostTime));
    }

    IEnumerator DashBooster(float boostAmount, float time)
    {
        playerController.dashForce *= boostAmount;
        yield return new WaitForSeconds(time);
        playerController.dashForce = playerStartDashForce;
        Destroy(gameObject);
    }
}
