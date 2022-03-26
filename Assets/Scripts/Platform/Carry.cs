using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
            collision.gameObject.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
