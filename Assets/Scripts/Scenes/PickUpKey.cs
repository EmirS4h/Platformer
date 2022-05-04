using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.Instance.playerHaveTheKey = true;
        gameObject.SetActive(false);
        gameManager.objects.Add(gameObject);
    }
}
