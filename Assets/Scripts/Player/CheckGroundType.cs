using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundType : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grass"))
        {
            SoundManager.Instance.groundType = 0;
        }
        else if (collision.gameObject.CompareTag("Stone"))
        {
            SoundManager.Instance.groundType = 1;
        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            SoundManager.Instance.groundType = 2;
        }
    }
}
