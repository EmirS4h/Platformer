using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // OYUNCUNUN LEVEL ICERISINDEKI DURUMUNU ve YAPDIKLARINI TAKIP EDER
    public static GameManager Instance;

    Rigidbody2D rb;
    AudioSource audioSource;

    public Vector2 playerStartingPosition;
    public Vector3 playerStartingRotation;
    public float playerStartingDirection;
    public bool playerStartingFacingDirection;

    public float playerStartingSpeed;
    public float playerStartingJumpForce;
    public float playerStartingDashForce;

    public List<GameObject> objects = new List<GameObject>();
    public List<GameObject> boostObject = new List<GameObject>();
    public List<GameObject> boostObjectFromChest = new List<GameObject>();

    public List<OpenChest> chestsOpened = new List<OpenChest>();
    public List<LevelKey> keysActivated = new List<LevelKey>();

    public GameObject booster;
    public Transform[] boosterSpawnPoints;

    public GameObject nextLevelGate;
    public Transform[] rndomPositions;

    public GameObject[] playerModels;
    public Transform startingPosition;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject prefab = playerModels[PlayerPrefs.GetInt("charIndex")]; // Secili Karakter Modelini Bul
            Instantiate(prefab, startingPosition.position, Quaternion.identity); // Instantiate the prefferd char model

            // Get The Starting Stats Of The Player
            playerStartingPosition = new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y);
            playerStartingRotation = new Vector3(PlayerController.Instance.transform.rotation.x, PlayerController.Instance.transform.rotation.y, PlayerController.Instance.transform.rotation.z);
            playerStartingDirection = PlayerController.Instance.direction;
            playerStartingFacingDirection = PlayerController.Instance.isFacingRight;

            playerStartingSpeed = PlayerController.Instance.moveSpeed;
            playerStartingDashForce = PlayerController.Instance.dashForce;
            playerStartingJumpForce = PlayerController.Instance.jumpForce;

            rb = PlayerController.Instance.GetComponent<Rigidbody2D>(); // Get the Chars RigidBody

            audioSource = GetComponent<AudioSource>();
        }

    }

    // Oyuncunun baslangic pozisyonunu ve ozelliklerini kaydeder
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            SpawnRandomObject(booster, boosterSpawnPoints);
            SpawnRandomObject(nextLevelGate, rndomPositions);
            audioSource.Play();
        }
        StartTime();
    }

    // Oyuncu olunce Oyuncunun olmeden once topladigi herseyi geri getirir
    public void ReActivateBack()
    {
        foreach (GameObject o in objects)
        {
            o.SetActive(true);
        }
        objects.Clear();

        foreach (GameObject o in boostObject)
        {
            o.GetComponent<SpriteRenderer>().enabled = true;
            o.GetComponent<BoxCollider2D>().enabled = true;
        }
        boostObject.Clear();

        foreach (OpenChest o in chestsOpened)
        {
            o.ReturnNormalState();
        }

        foreach (GameObject o in boostObjectFromChest)
        {
            Destroy(o);
        }

        foreach (LevelKey key in keysActivated)
        {
            key.DeactivateKey();
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
        PlayerController.Instance.boostActive = false;
    }

    // For spawning Object to random positions
    public void SpawnRandomObject(GameObject gameObject, Transform[] spawnPoints)
    {
        if (gameObject && spawnPoints.Length != 0)
        {
            int rndPos = Random.Range(0, spawnPoints.Length);
            Instantiate(gameObject, spawnPoints[rndPos].transform.position, gameObject.transform.rotation);
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }
    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }
}
