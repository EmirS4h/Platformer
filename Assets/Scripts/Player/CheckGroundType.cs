using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundType : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grass"))
        {
            soundManager.groundType = 0;
        }
        else if (collision.gameObject.CompareTag("Stone"))
        {
            soundManager.groundType = 1;
        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            soundManager.groundType = 2;
        }
    }
}
