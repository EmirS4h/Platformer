using System.Collections;
using UnityEngine;
public class TwoWayPlatform : MonoBehaviour
{
    [SerializeField] PlayerActions playerActions = default;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite inActiveSprite;
    private BoxCollider2D bxcollider2D;
    private bool playerOnPlatform = false;
    private WaitForSeconds delay;

    [SerializeField] Vector3 startingPos;
    [SerializeField] ActivatePlatform cmp;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        bxcollider2D = GetComponent<BoxCollider2D>();
        delay = new WaitForSeconds(0.5f);
        startingPos = transform.position;
    }
    private void OnEnable()
    {
        playerActions.dropDownEvent += DropDownPlatform;
    }
    private void OnDisable()
    {
        playerActions.dropDownEvent -= DropDownPlatform;
    }
    private void DropDownPlatform()
    {
        if (playerOnPlatform)
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
    public void ResetPlatform()
    {
        if (cmp != null)
        {
            transform.position = startingPos;
            DeActivatePlatform();
            cmp.DeactivateComputer();
            cmp.stoppedOnce = false;
        }
    }
}
