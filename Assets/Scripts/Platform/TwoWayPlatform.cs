using System.Collections;
using UnityEngine;
public class TwoWayPlatform : MonoBehaviour
{
    private BoxCollider2D bxcollider2D;
    private bool playerOnPlatform = false;

    private void Awake()
    {
        bxcollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //CrossPlatformInputManager.GetButtonDown("DropBtn")
        if (playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            bxcollider2D.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        bxcollider2D.enabled = true;
    }
}
