using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    [SerializeField] PlayerActions playerActions = default;
    [SerializeField] TwoWayPlatform twp;
    [SerializeField] Tween tween;
    [SerializeField] bool computerActive = false;
    [SerializeField] bool playerOnComputer = false;
    [SerializeField] bool stoppedOnce = false;

    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite activeComputer;
    [SerializeField] Sprite inActiveComputer;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerOnComputer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnComputer = false;
    }
    private void OnEnable()
    {
        playerActions.interactEvent += ActivateComputer;
    }
    private void OnDisable()
    {
        playerActions.interactEvent -= ActivateComputer;
    }
    private void ActivateComputer()
    {
        if (!computerActive && playerOnComputer)
        {
            spr.sprite = activeComputer;
            twp.ActivatePlatform();
            if (!stoppedOnce)
            {
                tween.MoveLeftRight();
                stoppedOnce = true;
            }
            else
            {
                tween.ResumeTween();
            }
            computerActive = true;
            tween.enabled = true;
        }
        else if (computerActive && playerOnComputer)
        {
            DeactivateComputer();
        }
    }
    public void DeactivateComputer()
    {
        if (computerActive &&  playerOnComputer)
        {
            spr.sprite = inActiveComputer;
            twp.DeActivatePlatform();
            tween.CancelTween();
            computerActive = false;
            tween.enabled = true;
        }
    }
}
