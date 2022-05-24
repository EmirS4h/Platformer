using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite closedChestSprite;
    public Sprite openedChestSprite;
    [SerializeField] private bool isPlayerOnTheChest = false;
    public bool chestOpened = false;
    [SerializeField] GameObject[] objects;
    GameObject interactBtn;
    public enum ChestType
    {
        Gold,
        Silver,
    }
    public ChestType type;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactBtn = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnTheChest && !chestOpened)
        {
            interactBtn.SetActive(true);
        }
        else
        {
            interactBtn.SetActive(false);
        }

        if (isPlayerOnTheChest && !chestOpened && Input.GetKeyDown(KeyCode.E))
        {
            spriteRenderer.sprite = openedChestSprite;
            chestOpened = true;
            int rnd = Random.Range(0, objects.Length);
            int leftRight = Random.Range(0, 2);
            Vector2 spawnPos = new Vector2(leftRight == 0 ? gameObject.transform.position.x + 1.0f : gameObject.transform.position.x + -1.0f, gameObject.transform.position.y);
            Instantiate(objects[rnd], spawnPos, Quaternion.identity);
            if (type == ChestType.Silver)
            {
                GameManager.Instance.chestsOpened.Add(this);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerOnTheChest = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isPlayerOnTheChest = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerOnTheChest = false;
    }

    public void ReturnNormalState()
    {
        spriteRenderer.sprite = closedChestSprite;
        chestOpened = false;
    }
}
