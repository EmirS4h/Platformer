using System.Collections;
using UnityEngine;
public class TwoWayPlatform : MonoBehaviour
{
    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite inActiveSprite;
    private BoxCollider2D bxcollider2D;
    private bool playerOnPlatform = false;
    private WaitForSeconds delay;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        bxcollider2D = GetComponent<BoxCollider2D>();
        delay = new WaitForSeconds(0.5f);
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
        playerOnPlatform = false;
    }
    private IEnumerator EnableCollider()
    {
        yield return delay;
        bxcollider2D.enabled = true;
    }
    public void ActivatePlatform()
    {
        spr.sprite = activeSprite;
    }
    public void DeActivatePlatform()
    {
        spr.sprite = inActiveSprite;
    }
}
