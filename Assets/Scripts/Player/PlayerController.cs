using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SoundManager soundManager;

    [Header("Player Particles")]
    [SerializeField] private ParticleSystem jumpParticle;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private ParticleSystem wallSlideParticle;

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
    public bool playerHaveTheKey = false;

    public int collectedDashBoosterAmount = 0;
    public int collectedJumpBoosterAmount = 0;
    public int collectedDashForceBoosterAmount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();
        wallJumpDirection.Normalize();
        LoadPlayerData();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        ApplyFriction();

        if (!isGameOver)
        {
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
                canDash = false;
            }
        }
    }

    void Update()
    {
        ChangeAnimation();

        if (!isGameOver)
        {
            #region Check for Horizontal Input

            // Mobile
            //horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");

            // Pc Web
            horizontalInput = Input.GetAxisRaw("Horizontal");
            #endregion


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
            soundManager.playLandingSound = !isGrounded;
            CheckWall();
            CheckForWallSliding();

            #endregion

            #region Jump

            if (isGrounded || wallSliding)
            {
                canJump = true;
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
                coyoteTimeCounter -= Time.deltaTime; // for jumping
            }
            // Input.GetButtonDown("Jump") PC Web
            // Mobile CrossPlatformInputManager.GetButtonDown("Jump")
            if (Input.GetButtonDown("Jump") && (isGrounded || wallSliding))
            {
                jumpBufferTimeCounter = bufferTime;
            }
            else if (Input.GetButtonDown("Jump") && canDoubleJump)
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

            if (Input.GetButtonDown("Jump") && rb.velocity.y > 0.01f)
            {
                coyoteTimeCounter = 0.0f;
            }
            #endregion

            #region Dash
            // Input.GetButtonDown("Dash")
            // CrossPlatformInputManager.GetButtonDown("Dash")
            if (Input.GetButtonDown("Dash") && canDash && dashsLeft != 0)
            {
                isDashing = true;
                canDash = false;
                dashsLeft--;
                if (Mathf.Abs(horizontalInput)>0.01f)
                {
                    dashParticle.Play();
                    CameraShake.Instance.Shake(cameraShakeIntensity, cameraShakeTime);
                }
                StartCoroutine(StopDash());
            }
            if (isGrounded || wallSliding)
                canDash = false;
            else
                canDash = true;

            #endregion
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
            soundManager.PlayJumpSound();
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
    }

    private void ApplyGravity()
    {
        if (wallSliding)
        {
            rb.gravityScale = wallSlideGravity;
        }
        // Short jump
        // !Input.GetButton("Jump")
        // !CrossPlatformInputManager.GetButton("Jump")
        else if (rb.velocity.y > 0.01f && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else if (rb.velocity.y < -0.01f) // Long jump
        {
            rb.gravityScale = fallMultiplier;
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

    public void LoadPlayerData()
    {
        PlayerData data = SaveData.LoadPlayerData();
        jumpForce = data.playerJumpForce;
        moveSpeed = data.playerSpeed;
        dashForce = data.playerDashForce;

        hasDoubleJump = data.hasDoubleJump;
        maxDash = data.playerMaxDash;

        collectedDashBoosterAmount = data.collectedDashBoosterAmount;
        collectedJumpBoosterAmount = data.collectedJumpBoosterAmount;
        collectedDashForceBoosterAmount = data.collectedDashForceBoosterAmount;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGround.position, checkGroundRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
