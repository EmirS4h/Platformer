using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.playerHaveTheKey = true;
        Destroy(gameObject);
    }
}
