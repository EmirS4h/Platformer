using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // OYUNCUNUN LEVEL ICERISINDEKI DURUMUNU ve YAPDIKLARINI TAKIP EDER
    public static GameManager Instance;

    [SerializeField] PlayerController playerController;
    Rigidbody2D rb;

    public Vector2 playerStartingPosition;
    public Vector3 playerStartingRotation;
    public float playerStartingDirection;
    public bool playerStartingFacingDirection;

    public float playerStartingSpeed;
    public float playerStartingJumpForce;
    public float playerStartingDashForce;

    public List<GameObject> objects = new List<GameObject>();
    public List<GameObject> boostObject = new List<GameObject>();

    public GameObject[] boosters;
    public Transform[] boosterSpawnPoints;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Oyuncunun baslangic pozisyonunu ve ozelliklerini kaydeder
    private void Start()
    {
        playerStartingPosition = new Vector2(playerController.transform.position.x, playerController.transform.position.y);
        playerStartingRotation = new Vector3(playerController.transform.rotation.x, playerController.transform.rotation.y, playerController.transform.rotation.z);
        playerStartingDirection = playerController.direction;
        playerStartingFacingDirection = playerController.isFacingRight;

        playerStartingSpeed = playerController.moveSpeed;
        playerStartingDashForce = playerController.dashForce;
        playerStartingJumpForce = playerController.jumpForce;

        rb = playerController.GetComponent<Rigidbody2D>();

        SpawnRandomObject(boosters, boosterSpawnPoints);
    }

    // Oyuncu olunce Oyuncunun olmeden once topladigi herseyi geri getirir
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

    // Oyuncuyu baslangictaki haline dondurur
    public void SentPlayerBackToStart()
    {
        playerController.transform.position = playerStartingPosition;
        playerController.transform.rotation = Quaternion.Euler(playerStartingRotation);
        playerController.direction = playerStartingDirection;
        playerController.isFacingRight = playerStartingFacingDirection;

        playerController.moveSpeed = playerStartingSpeed;
        playerController.jumpForce = playerStartingJumpForce;
        playerController.dashForce = playerStartingDashForce;

        rb.bodyType = RigidbodyType2D.Dynamic;
        playerController.playerHaveTheKey = false;
    }

    public void SpawnRandomObject(GameObject[] gameObjects, Transform[] spawnPoints)
    {
        if(gameObjects.Length != 0 && spawnPoints.Length != 0)
        {
            int rndPos = Random.Range(0, spawnPoints.Length);
            int rndObj = Random.Range(0, gameObjects.Length);
            Instantiate(gameObjects[rndObj], spawnPoints[rndPos].transform.position, gameObjects[rndObj].transform.rotation);
        }
    }
}
