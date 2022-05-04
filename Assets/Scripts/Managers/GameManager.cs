using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // OYUNCUNUN LEVEL ICERISINDEKI DURUMUNU ve YAPDIKLARINI TAKIP EDER
    public static GameManager Instance;

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
        playerStartingPosition = new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y);
        playerStartingRotation = new Vector3(PlayerController.Instance.transform.rotation.x, PlayerController.Instance.transform.rotation.y, PlayerController.Instance.transform.rotation.z);
        playerStartingDirection = PlayerController.Instance.direction;
        playerStartingFacingDirection = PlayerController.Instance.isFacingRight;

        playerStartingSpeed = PlayerController.Instance.moveSpeed;
        playerStartingDashForce = PlayerController.Instance.dashForce;
        playerStartingJumpForce = PlayerController.Instance.jumpForce;

        rb = PlayerController.Instance.GetComponent<Rigidbody2D>();

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
        PlayerController.Instance.transform.position = playerStartingPosition;
        PlayerController.Instance.transform.rotation = Quaternion.Euler(playerStartingRotation);
        PlayerController.Instance.direction = playerStartingDirection;
        PlayerController.Instance.isFacingRight = playerStartingFacingDirection;

        PlayerController.Instance.moveSpeed = playerStartingSpeed;
        PlayerController.Instance.jumpForce = playerStartingJumpForce;
        PlayerController.Instance.dashForce = playerStartingDashForce;

        rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerController.Instance.playerHaveTheKey = false;
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
