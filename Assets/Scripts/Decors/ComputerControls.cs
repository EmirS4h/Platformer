using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerControls : MonoBehaviour
{
    [SerializeField] PlayerActions playerActions = default;

    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite inactiveSprite;
    [SerializeField] Sprite activeSprite;

    [SerializeField] bool computerActivated = false;
    [SerializeField] bool playerOnComputer = false;
    [SerializeField] CastLaser[] lasers;

    private void OnEnable()
    {
        playerActions.interactEvent += ComputerInteraction;
    }
    private void OnDisable()
    {
        playerActions.interactEvent -= ComputerInteraction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerOnComputer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnComputer = false;
    }
    private void ComputerInteraction()
    {
        if (playerOnComputer && !computerActivated)
        {
            computerActivated = true;
            spr.sprite = activeSprite;
            foreach (var item in lasers)
            {
                item.DeactivateLineRenderer();
            }
        }
    }
    public void DeactivateComputer()
    {
        spr.sprite = inactiveSprite;
        computerActivated = false;
    }
}
