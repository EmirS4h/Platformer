using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.playerHaveTheKey = true;
        gameManager.objects.Add(gameObject);
        gameObject.SetActive(false);
    }
}
