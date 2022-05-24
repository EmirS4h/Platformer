using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKey : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    [SerializeField] Sprite inActiveSprite;
    [SerializeField] Sprite activeSprite;
    [SerializeField] GameObject bg, interactBtn;
    SpriteRenderer spriteRenderer;

    [SerializeField] bool playerOnKey = false;
    public bool keyActivated = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactBtn = transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        if (playerOnKey && Input.GetKeyDown(KeyCode.E))
        {
            spriteRenderer.sprite = activeSprite;
            particles.Play();
            bg.SetActive(true);
            keyActivated = true;
            GameManager.Instance.keysActivated.Add(this); // Adds its self to GameManagers activeKeysList
        }

        if(!keyActivated && playerOnKey)
        {
            interactBtn.SetActive(true);
        }
        else
        {
            interactBtn.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerOnKey = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerOnKey = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnKey = false;
    }

    public void DeactivateKey()
    {
        spriteRenderer.sprite = inActiveSprite;
        bg.SetActive(false);
        keyActivated = false;
        particles.Stop();
    }
}
