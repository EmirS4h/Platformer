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

    [SerializeField] bool openClose = false;

    WaitForSeconds _delay;
    private void Awake()
    {
        _delay = new WaitForSeconds(timeToClose);

        time = timeToClose;
        spr.sprite = newSprite;
    }
    private void Update()
    {
        if (openClose)
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
    private IEnumerator DeActivateTrap()
    {
        trapToClose.SetActive(false);
        yield return _delay;
        time = timeToClose;
    }
    public void CloseTrap()
    {
        trapToClose.SetActive(false);
    }
}
