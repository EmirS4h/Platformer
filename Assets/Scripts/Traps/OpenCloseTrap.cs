using System.Collections;
using UnityEngine;

public class OpenCloseTrap : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float timeToClose;

    [SerializeField] GameObject trapToClose;

    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite newSprite;
    [SerializeField] Sprite oldSprite;

    private void Start()
    {
        time = timeToClose;
    }

    private void Update()
    {
        if (time > 0)
        {
            trapToClose.SetActive(true);
            spr.sprite = newSprite;
            time -= Time.deltaTime;
        }
        else
        {
            spr.sprite = oldSprite;
            StartCoroutine(DeActivateTrap());
        }
    }
    private IEnumerator ActivateTrap()
    {
        trapToClose.SetActive(true);
        yield return new WaitForSeconds(timeToClose);
    }
    private IEnumerator DeActivateTrap()
    {
        trapToClose.SetActive(false);
        yield return new WaitForSeconds(timeToClose);
        time = timeToClose;
    }
}
