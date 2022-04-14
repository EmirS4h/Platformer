using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    Rigidbody2D rb;
    public Vector2 playerStartingPosition;
    public Vector3 playerStartingRotation;
    public float playerStartingDirection;
    public bool playerStartingFacingDirection;
    public List<GameObject> objects = new List<GameObject>();
    public List<GameObject> boostObject = new List<GameObject>();

    private void Start()
    {
        playerStartingPosition = new Vector2(playerController.transform.position.x, playerController.transform.position.y);
        playerStartingRotation = new Vector3(playerController.transform.rotation.x, playerController.transform.rotation.y, playerController.transform.rotation.z);
        playerStartingDirection = playerController.direction;
        playerStartingFacingDirection = playerController.isFacingRight;
        rb = playerController.GetComponent<Rigidbody2D>();
    }

    public void ReActivateBack()
    {
        if (objects.Count > 0)
        {
            foreach (GameObject o in objects)
            {
                o.SetActive(true);
            }
            objects.Clear();
        }

        if (boostObject.Count > 0)
        {
            foreach (GameObject o in boostObject)
            {
                o.SetActive(true);
                o.GetComponent<SpriteRenderer>().enabled = true;
                o.GetComponent<BoxCollider2D>().enabled = true;
            }
            boostObject.Clear();
        }
    }
    public void SentPlayerBackToStart()
    {
        playerController.transform.position = playerStartingPosition;
        playerController.transform.rotation = Quaternion.Euler(playerStartingRotation);
        playerController.direction = playerStartingDirection;
        playerController.isFacingRight = playerStartingFacingDirection;
        rb.bodyType = RigidbodyType2D.Dynamic;
        playerController.playerHaveTheKey = false;
    }
}
