using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public AudioSource audioSource;
    public PlayerLife playerLife;
    public BoxCollider2D bxCollider;

    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer spr;
    [SerializeField] PlayerActions playerActions;
    [SerializeField] Light2D light2D;

    [Header("Player Particles")]
    [SerializeField] private ParticleSystem jumpParticle;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private ParticleSystem wallSlideParticle;
    public ParticleSystem bloodParticle;

    [Header("Player Ground Checking")]
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkGroundRadius;

    [Header("Player Settings")]
    public float moveSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float frictionAmount;

    [Header("Player Jump")]
    public bool isGrounded = false;
    [SerializeField] private bool canJump;
    public float jumpForce;
    [SerializeField] bool doubleJump = false;
    [SerializeField] bool canDoubleJump = false;
    public bool hasDoubleJump = false;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float bufferTime;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float coyoteTimeCounter;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float jumpBufferTimeCounter;

    [Header("Player Wall Jump / Slide")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallSlideGravity;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool wallSliding;
    [SerializeField] private Vector2 wallJumpDirection;
    [SerializeField] private float wallJumpForce;
    private Vector2 wallJumpForceToAdd;
    public float direction = 1;

    [Header("Player Dash")]
    [SerializeField] AudioClip dashSound;
    public float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float cameraShakeIntensity;
    [SerializeField] private float cameraShakeTime;
    public int maxDash;
    [SerializeField] private int dashsLeft;
    public bool canDash = false;
    private bool isDashing = false;

    [Header("Player Movement")]
    [SerializeField] private float amount;
    [SerializeField] private float movement;
    [SerializeField] private float speedDif;
    [SerializeField] private float accelRate;
    [SerializeField] private float targetSpeed;
    [SerializeField] private float horizontalInput;

    private bool isJumping = false;
    public bool isFacingRight = true;

    public int groundType = 0;

    public bool isGameOver = false;
    //public bool playerHaveTheKey = false;

    public bool collectedDoubleDashBooster = false;
    public bool collectedDoubleJumpBooster = false;

    public bool collectedDashForceBooster = false;
    public bool collectedJumpForceBooster = false;
    public bool collectedMoveSpeedBooster = false;

    public bool boostActive = false;

    public bool hasCollectedPowerUpStone = false;

    [SerializeField] GameObject tpStone;
    [SerializeField] GameObject tpStoneInstance;
    [SerializeField] Vector3 tpStoneCoords;
    public bool tpStonePlaced = false;

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
        LoadPlayerData();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerLife = GetComponent<PlayerLife>();
        bxCollider = GetComponent<BoxCollider2D>();
        wallJumpDirection.Normalize();

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            light2D.enabled = true;
        }
        else
        {
            light2D.enabled = false;
        }
    }
    private void OnEnable()
    {
        playerActions.movementEvent += OnMove;
        playerActions.dashEvent += OnDash;
        playerActions.jumpEvent += OnJump;
        playerActions.placeTpStoneEvent += OnTeleportStonePlace;
        playerActions.tpBackEvent += OnTpBack;
    }

    private void OnDisable()
    {
        playerActions.movementEvent -= OnMove;
        playerActions.dashEvent -= OnDash;
        playerActions.jumpEvent -= OnJump;
        playerActions.placeTpStoneEvent -= OnTeleportStonePlace;
        playerActions.tpBackEvent -= OnTpBack;
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            ApplyFriction();
            Move();

            if (isJumping)
            {
                Jump();
                isJumping = false;
            }
            if (doubleJump)
            {
                Jump();
                doubleJump = false;
                canDoubleJump = false;
            }
            if (isDashing)
            {
                rb.AddForce(new Vector2(horizontalInput, 0.0f) * dashForce, ForceMode2D.Impulse);
                rb.gravityScale = 0.0f;
                canDash = false;
            }
        }
    }
    private void OnTpBack()
    {
        if (tpStonePlaced)
        {
            transform.position = tpStoneCoords;
        }
    }
    private void OnTeleportStonePlace()
    {
        if (!tpStonePlaced)
        {
            tpStoneCoords = transform.position;
            tpStoneInstance = Instantiate(tpStone, transform.position, Quaternion.identity);
            tpStonePlaced = true;
        }
    }
    public void DestroyTpStone()
    {
        Destroy(tpStoneInstance);
    }
    private void OnMove(Vector2 movement)
    {
        horizontalInput = movement.x;
    }
    private void OnDash()
    {
        if (canDash && dashsLeft != 0)
        {
            isDashing = true;
            canDash = false;
            dashsLeft--;
            if (Mathf.Abs(horizontalInput)>0.01f)
            {
                dashParticle.Play();
                audioSource.PlayOneShot(dashSound);
                CameraShake.Instance.Shake(cameraShakeIntensity, cameraShakeTime);
            }
            StartCoroutine(StopDash());
        }
    }
    private void OnJump()
    {
        if (isGrounded || wallSliding)
        {
            jumpBufferTimeCounter = bufferTime;
        }
        else if (canDoubleJump)
        {
            doubleJump = true;
        }
        else
        {
            jumpBufferTimeCounter -= Time.deltaTime;
        }

        if (jumpBufferTimeCounter > 0.01f && coyoteTimeCounter > 0.01f)
        {
            isJumping = true;
            jumpBufferTimeCounter = 0.0f;
        }

        if (rb.velocity.y > 0.01f)
        {
            coyoteTimeCounter = 0.0f;
        }


    }
    void Update()
    {
        ApplyGravity();

        ChangeAnimation();

        if (!isGameOver)
        {

            #region Flip the Character based on HorizontalInput

            if (horizontalInput > 0 && !isFacingRight)
            {
                FlipCharacter();
            }
            else if (horizontalInput < 0 && isFacingRight)
            {
                FlipCharacter();
            }
            #endregion

            #region Check for Surroundings

            CheckGround();
            CheckWall();
            CheckForWallSliding();

            #endregion

            if (isGrounded || wallSliding)
            {
                canJump = true;
                canDash = false;
                if (hasDoubleJump)
                {
                    canDoubleJump = true;
                }
                coyoteTimeCounter = coyoteTime; // for jumping
                dashsLeft = maxDash;
            }
            else
            {
                canJump = false;
                canDash = true;
                coyoteTimeCounter -= Time.deltaTime; // for jumping
            }

            if (transform.position.y < -50.0f)
            {
                GameManager.Instance.ReActivateBack();
                GameManager.Instance.SentPlayerBackToStart();
            }
        }
        else
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    private void ChangeAnimation()
    {
        if (!isGameOver)
        {
            animator.SetBool("Running", horizontalInput != 0);
            animator.SetBool("Jumping", rb.velocity.y > 0.01f);
            animator.SetBool("Falling", rb.velocity.y < -0.01f);
        }
        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkGroundRadius, groundLayer);
    }
    private void CheckWall()
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundLayer);
    }

    private void CheckForWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0.01f)
        {
            wallSliding = true;
            wallSlideParticle.Play();
        }
        else
        {
            wallSliding = false;
        }
    }

    private void Move()
    {
        targetSpeed = horizontalInput * moveSpeed;
        speedDif = targetSpeed - rb.velocity.x;
        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 0.9f) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private void Jump()
    {
        if (!wallSliding)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpParticle.Play();
            SoundManager.Instance.PlayJumpSound();
        }
        else if (wallSliding && canJump)
        {
            wallSliding = false;
            wallJumpForceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -direction, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(wallJumpForceToAdd, ForceMode2D.Impulse);
            FlipCharacter();
        }
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.gravityScale = gravityScale;
    }

    private void ApplyGravity()
    {
        if (wallSliding)
        {
            rb.gravityScale = wallSlideGravity;
        }
        else if (rb.velocity.y < -0.01f)
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void ApplyFriction()
    {
        if (isGrounded && Mathf.Abs(horizontalInput) < 0.01f)
        {
            amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private void FlipCharacter()
    {
        direction *= -1;

        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void SavePlayerData()
    {
        SaveData.SavePlayerData(this);
    }

    public void ResetPlayerData()
    {
        jumpForce = 12.0f;
        moveSpeed = 12.0f;
        dashForce = 6.0f;

        hasDoubleJump = false;
        canDoubleJump = false;
        maxDash = 1;

        collectedDoubleDashBooster = false;
        PlayerPrefs.DeleteKey("hasCollectedDoubleDashBooster");
        collectedDoubleJumpBooster = false;
        PlayerPrefs.DeleteKey("hasCollectedDoubleJumpBooster");
        collectedDashForceBooster = false;
        PlayerPrefs.DeleteKey("hasCollectedDashForceBooster");
        collectedJumpForceBooster = false;
        PlayerPrefs.DeleteKey("hasCollectedMoveSpeedBooster");
        collectedMoveSpeedBooster = false;
        PlayerPrefs.DeleteKey("hasCollectedJumpForceBooster");
        hasCollectedPowerUpStone = false;
        PlayerPrefs.DeleteKey("hasCollectedPowerUpStone");

        PlayerPrefs.DeleteKey("wallJumpNotif");
        PlayerPrefs.DeleteKey("dashNotif");
        PlayerPrefs.DeleteKey("totemNotif");
        PlayerPrefs.DeleteKey("potionNotif");
        PlayerPrefs.DeleteKey("nextLevelDoorNotif");
        PlayerPrefs.DeleteKey("selectedLevelIndex");
    }
    public void LoadPlayerData()
    {
        PlayerData data = SaveData.LoadPlayerData();
        jumpForce = data.playerJumpForce;
        moveSpeed = data.playerSpeed;
        dashForce = data.playerDashForce;

        hasDoubleJump = data.hasDoubleJump;
        maxDash = data.playerMaxDash;

        collectedDoubleDashBooster = data.collectedDashBoosterAmount;
        collectedDoubleJumpBooster = data.collectedJumpBoosterAmount;

        collectedDashForceBooster = data.collectedDashForceBoosterAmount;
        collectedJumpForceBooster = data.collectedJumpForceBoosterAmount;
        collectedMoveSpeedBooster = data.collectedMoveSpeedBoosterAmount;

        hasCollectedPowerUpStone = data.hasCollectedPowerUpStone;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGround.position, checkGroundRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
