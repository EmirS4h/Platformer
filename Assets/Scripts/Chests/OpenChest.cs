using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class OpenChest : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite closedChestSprite;
    public Sprite openedChestSprite;
    [SerializeField] private bool isPlayerOnTheChest = false;
    public bool chestOpened = false;
    [SerializeField] GameObject[] objects;
    GameObject interactBtn;

    [SerializeField] PlayerActions playerActions = default;
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

    private void OnEnable()
    {
        playerActions.interactEvent += Open;
    }
    private void OnDisable()
    {
        playerActions.interactEvent -= Open;
    }

    public void Open()
    {
        if (isPlayerOnTheChest && !chestOpened)
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        isPlayerOnTheChest = true;

        if (!chestOpened)
        {
            interactBtn.SetActive(true);
        }
        else
        {
            interactBtn.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerOnTheChest = false;
        interactBtn.SetActive(false);
    }

    public void ReturnNormalState()
    {
        spriteRenderer.sprite = closedChestSprite;
        chestOpened = false;
    }
}
