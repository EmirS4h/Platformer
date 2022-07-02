using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActivatePlatform : MonoBehaviour
{
    [SerializeField] PlayerActions playerActions = default;
    [SerializeField] TwoWayPlatform twp;
    [SerializeField] Tween tween;
    [SerializeField] bool computerActive = false;
    [SerializeField] bool playerOnComputer = false;
    [SerializeField] bool stoppedOnce = false;
    [SerializeField] bool shouldPause = false;

    [SerializeField] Light2D cmpLight;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite activeComputer;
    [SerializeField] Sprite inActiveComputer;

    [SerializeField] AudioSource audioSource;

    public enum MoveSide
    {
        UpDown, LeftRight
    }
    public MoveSide side = MoveSide.LeftRight;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOnComputer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
            cmpLight.enabled = true;
            if (!stoppedOnce)
            {
                switch (side)
                {
                    case MoveSide.UpDown:
                        tween.MoveUpDown();
                        break;
                    case MoveSide.LeftRight:
                        tween.MoveLeftRight();
                        break;
                    default:
                        break;
                }
                stoppedOnce = true;
            }
            else
            {
                tween.ResumeTween();
            }
            computerActive = true;
            tween.enabled = true;
            audioSource.Play();
        }
        else if (computerActive && playerOnComputer)
        {
            DeactivateComputer();
            audioSource.Play();
        }
    }
    public void DeactivateComputer()
    {
        spr.sprite = inActiveComputer;
        cmpLight.enabled = false;
        stoppedOnce = false;
        computerActive = false;
        tween.enabled = true;
        twp.DeActivatePlatform();
        if (shouldPause)
        {
            tween.PauseTween();
        }
        else
        {
            tween.CancelTween();
        }
    }
}
