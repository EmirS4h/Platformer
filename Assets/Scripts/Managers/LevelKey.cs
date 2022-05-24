using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKey : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    [SerializeField] Sprite inActiveSprite;
    [SerializeField] Sprite activeSprite;
    [SerializeField] GameObject bg;
    SpriteRenderer spriteRenderer;

    [SerializeField] bool playerOnKey = false;
    public bool keyActivated = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
