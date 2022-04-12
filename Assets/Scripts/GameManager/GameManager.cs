using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    Rigidbody2D rb;
    public Vector2 playerStartingPosition;
    public List<GameObject> objects = new List<GameObject>();

    private void Start()
    {
        playerStartingPosition = new Vector2(playerController.transform.position.x, playerController.transform.position.y);
        rb = playerController.GetComponent<Rigidbody2D>();
    }

    public void ReActivateBack()
    {
        if(objects.Count > 0)
        {
            foreach (GameObject o in objects)
            {
                o.SetActive(true);
            }
            objects.Clear();
        }
    }
    public void SentPlayerBackToStart()
    {
        playerController.transform.position = playerStartingPosition;
        rb.bodyType = RigidbodyType2D.Dynamic;
        playerController.playerHaveTheKey = false;
    }
}
