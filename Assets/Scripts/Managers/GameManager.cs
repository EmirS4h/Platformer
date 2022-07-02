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

    public bool portalOpened = false;

    public Vector2 playerStartingPosition;
    public Vector3 playerStartingRotation;
    public float playerStartingDirection;
    public bool playerStartingFacingDirection;

    public float playerStartingSpeed;
    public float playerStartingJumpForce;
    public float playerStartingDashForce;

    public List<GameObject> objects = new();
    public List<GameObject> boostObject = new();
    public List<GameObject> boostObjectFromChest = new();

    public List<OpenChest> chestsOpened = new();
    public List<LevelKey> keysActivated = new();

    [SerializeField] private GameObject nextLevelGate;
    public GameObject portal;
    public GameObject portalOrb;
    [SerializeField] private Transform[] randomGatePositions;

    [SerializeField] private GameObject[] playerModels;
    [SerializeField] private Transform startingPosition;

    public LevelKey[] keys;
    public CastLaser[] lasers;
    public ComputerControls[] computers;
    public TwoWayPlatform[] platforms;

    [SerializeField] Transform boosterSpawnPoint;
    [SerializeField] GameObject booster;
    [SerializeField] bool spawnDoubleJumpBooster = false;
    [SerializeField] bool spawnDoubleDashBooster = false;
    [SerializeField] bool spawnDashForceBooster = false;
    [SerializeField] bool spawnJumpForceBooster = false;
    [SerializeField] bool spawnMoveSpeedBooster = false;

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
            lasers = FindObjectsOfType<CastLaser>();
            computers = FindObjectsOfType<ComputerControls>();
            platforms = FindObjectsOfType<TwoWayPlatform>();

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
            audioSource.Play();

            if (spawnDoubleDashBooster && !PlayerPrefs.HasKey("hasCollectedDoubleDashBooster"))
            {
                Instantiate(booster, boosterSpawnPoint.position, Quaternion.identity);
            }
            else if (spawnDoubleJumpBooster && !PlayerPrefs.HasKey("hasCollectedDoubleJumpBooster"))
            {
                Instantiate(booster, boosterSpawnPoint.position, Quaternion.identity);
            }
            else if (spawnDashForceBooster && !PlayerPrefs.HasKey("hasCollectedDashForceBooster"))
            {
                Instantiate(booster, boosterSpawnPoint.position, Quaternion.identity);
            }
            else if (spawnMoveSpeedBooster && !PlayerPrefs.HasKey("hasCollectedMoveSpeedBooster"))
            {
                Instantiate(booster, boosterSpawnPoint.position, Quaternion.identity);
            }
            else if (spawnJumpForceBooster && !PlayerPrefs.HasKey("hasCollectedJumpForceBooster"))
            {
                Instantiate(booster, boosterSpawnPoint.position, Quaternion.identity);
            }
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
            totemsActivated = false;
            portalOpened = false;
            Destroy(portalOrb);
            Destroy(portal);
        }
        if (PlayerController.Instance.tpStonePlaced)
        {
            PlayerController.Instance.tpStonePlaced = false;
            PlayerController.Instance.DestroyTpStone();
        }
        foreach (var laser in lasers)
        {
            laser.ActivateLineRenderer();
        }
        foreach (var computer in computers)
        {
            computer.DeactivateComputer();
        }
        foreach (var platform in platforms)
        {
            platform.ResetPlatform();
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
        PlayerController.Instance.bxCollider.enabled = true;
        PlayerController.Instance.boostActive = false;
        UiManager.Instance.DeactivatePotImage();
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
        audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
        audioSource.pitch = 1.0f;
    }
}
