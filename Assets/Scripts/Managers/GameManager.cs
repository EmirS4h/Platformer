using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // OYUNCUNUN LEVEL ICERISINDEKI DURUMUNU ve YAPDIKLARINI TAKIP EDER
    public static GameManager Instance;

    public bool totemsActivated = false;

    Rigidbody2D rb;
    AudioSource audioSource;

    [SerializeField] AudioClip portalSound;

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

    [SerializeField] private GameObject doubleJumpBoosterPrefab;
    [SerializeField] private bool spawnDoubleJumpBooster;

    [SerializeField] private GameObject doubleDashBoosterPrefab;
    [SerializeField] private bool spawnDoubleDashBooster;

    [SerializeField] private GameObject permaMoveSpeedBoosterPrefab;
    [SerializeField] private bool spawnPermaMoveSpeedBooster;

    [SerializeField] private GameObject permaDashForceBoosterPrefab;
    [SerializeField] private bool spawnPermaDashForceBooster;

    [SerializeField] private GameObject permaJumpForceBoosterPrefab;
    [SerializeField] private bool spawnPermaJumpForceBooster;

    [SerializeField] private Transform[] boosterSpawnPoints;

    [SerializeField] private GameObject nextLevelGate;
    public GameObject portal;
    public GameObject portalOrb;
    [SerializeField] private Transform[] randomGatePositions;

    [SerializeField] private GameObject[] playerModels;
    [SerializeField] private Transform startingPosition;

    public LevelKey[] keys;
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
            keys = FindObjectsOfType<LevelKey>();

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

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (boosterSpawnPoints.Length > 0)
            {
                if (spawnDoubleJumpBooster && doubleJumpBoosterPrefab != null && !PlayerController.Instance.hasDoubleJump)
                {
                    SpawnRandomObject(doubleJumpBoosterPrefab, boosterSpawnPoints);
                }
                else if (spawnDoubleDashBooster && doubleDashBoosterPrefab != null && PlayerController.Instance.maxDash != 2)
                {
                    SpawnRandomObject(doubleDashBoosterPrefab, boosterSpawnPoints);
                }
                else if (spawnPermaMoveSpeedBooster && permaMoveSpeedBoosterPrefab != null && PlayerController.Instance.collectedMoveSpeedBoosterAmount != 3)
                {
                    SpawnRandomObject(permaMoveSpeedBoosterPrefab, boosterSpawnPoints);
                }
                else if (spawnPermaDashForceBooster && permaDashForceBoosterPrefab != null && PlayerController.Instance.collectedDashForceBoosterAmount != 3)
                {
                    SpawnRandomObject(permaDashForceBoosterPrefab, boosterSpawnPoints);
                }
                else if (spawnPermaJumpForceBooster && permaJumpForceBoosterPrefab != null && PlayerController.Instance.collectedJumpForceBoosterAmount != 3)
                {
                    SpawnRandomObject(permaJumpForceBoosterPrefab, boosterSpawnPoints);
                }
            }
            audioSource.Play();
        }
        StartTime();
        UiManager.Instance.DeactivateLoadingScreen();
    }

    // Oyuncu olunce Oyuncunun olmeden once topladigi herseyi geri getirir
    public void ReActivateBack()
    {
        if (objects.Count != 0)
        {
            foreach (GameObject o in objects)
            {
                o.SetActive(true);
            }
            objects.Clear();
        }

        if (boostObject.Count != 0)
        {
            foreach (GameObject o in boostObject)
            {
                o.GetComponent<SpriteRenderer>().enabled = true;
                o.GetComponent<BoxCollider2D>().enabled = true;
            }
            boostObject.Clear();
        }

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

        if (portalOrb)
        {
            Destroy(portalOrb);
        }
    }

    // Oyuncuyu baslangictaki haline dondurur
    public void SentPlayerBackToStart()
    {
        PlayerController.Instance.transform.SetPositionAndRotation(playerStartingPosition, Quaternion.Euler(playerStartingRotation));
        PlayerController.Instance.direction = playerStartingDirection;
        PlayerController.Instance.isFacingRight = playerStartingFacingDirection;

        PlayerController.Instance.moveSpeed = playerStartingSpeed;
        PlayerController.Instance.jumpForce = playerStartingJumpForce;
        PlayerController.Instance.dashForce = playerStartingDashForce;

        rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerController.Instance.boostActive = false;
        totemsActivated = false;
        Destroy(portal);
    }

    // For spawning Object to random positions
    public GameObject SpawnRandomObject(GameObject gameObject, Transform[] spawnPoints)
    {
        if (gameObject && spawnPoints.Length != 0)
        {
            int rndPos = Random.Range(0, spawnPoints.Length);
            GameObject ob = Instantiate(gameObject, spawnPoints[rndPos].transform.position, gameObject.transform.rotation);
            return ob;
        }
        else
        {
            return null;
        }
    }
    public void SpawnPortal()
    {
        portal = SpawnRandomObject(nextLevelGate, randomGatePositions);
        audioSource.PlayOneShot(portalSound);
    }
    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }
    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }
    public void PitchSoundDown()
    {
        audioSource.volume *= 0.5f;
        audioSource.pitch *= 0.5f;
    }
    public void DefaultPitch()
    {
        audioSource.volume = 0.2f;
        audioSource.pitch = 1.0f;
    }
}
