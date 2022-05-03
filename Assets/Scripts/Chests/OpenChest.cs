using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite newSprite;
    [SerializeField] private bool isPlayerOnTheChest = false;
    [SerializeField] private bool chestOpened = false;
    [SerializeField] GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnTheChest && !chestOpened && Input.GetKeyDown(KeyCode.E))
        {
            spriteRenderer.sprite = newSprite;
            chestOpened = true;
            int rnd = Random.Range(0, objects.Length);
            int leftRight = Random.Range(0, 2);
            Vector2 spawnPos = new Vector2(leftRight == 0 ? gameObject.transform.position.x + 0.8f : gameObject.transform.position.x + -0.8f, gameObject.transform.position.y);
            Instantiate(objects[rnd], spawnPos, objects[rnd].transform.rotation);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerOnTheChest = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerOnTheChest = false;
    }
}
