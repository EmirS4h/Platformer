using System.Collections;
using UnityEngine;

public class OpenCloseTrap : MonoBehaviour
{
    [SerializeField] float timeToClose;
    [SerializeField] float time;

    [SerializeField] GameObject trapToClose;
    [SerializeField] GameObject vfx;

    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite newSprite;
    [SerializeField] Sprite oldSprite;

    [SerializeField] bool openClose = false;
    [SerializeField] bool closed = false;

    private void Awake()
    {
        spr.sprite = newSprite;
        time = timeToClose;
    }
    private void Start()
    {
        InvokeRepeating(nameof(OpenTrap), 0f, timeToClose * 2);
    }
    private void Update()
    {
        if (openClose && !closed)
        {
            if (time < 0)
            {
                trapToClose.SetActive(false);
                vfx.SetActive(false);
                spr.sprite = oldSprite;
                closed = true;
            }
            time -= Time.deltaTime;
        }
    }
    public void CloseTrap()
    {
        trapToClose.SetActive(false);
    }
    public void OpenTrap()
    {
        spr.sprite = newSprite;
        trapToClose.SetActive(true);
        vfx.SetActive(true);
        closed = false;
        time = timeToClose;
    }
}
