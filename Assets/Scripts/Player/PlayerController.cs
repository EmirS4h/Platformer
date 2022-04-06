using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Player Particles")]
    [SerializeField] private ParticleSystem jumpParticle;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private ParticleSystem wallSlideParticle;

    [Header("Player Ground Checking")]
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float checkGroundRadius;

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float frictionAmount;

    [Header("Player Jump")]
    public bool isGrounded = false;
    [SerializeField] private bool canJump;
    [SerializeField] private int maxJump;
    [SerializeField] private int jumpsLeft;
    [SerializeField] private float jumpForce;
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
    private float direction = 1;

    [Header("Player Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float cameraShakeIntensity;
    [SerializeField] private float cameraShakeTime;
    [SerializeField] private int maxDash;
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
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        wallJumpDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Move();
        ApplyGravity();
        ApplyFriction();

        if (isJumping)
        {
            Jump();
            isJumping = false;
        }

        if (isDashing)
        {
            rb.AddForce(new Vector2(horizontalInput, 0.0f) * dashForce, ForceMode2D.Impulse);
            canDash = false;
        }
    }

    void Update()
    {
        #region Check for Horizontal Input

        horizontalInput = Input.GetAxisRaw("Horizontal");

        #endregion

        ChangeAnimation();

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

        #region Jump

        if (isGrounded || wallSliding)
        {
            canJump = true;
            coyoteTimeCounter = coyoteTime; // for jumping
            dashsLeft = maxDash;
        }
        else
        {
            canJump = false;
            coyoteTimeCounter -= Time.deltaTime; // for jumping
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimeCounter = bufferTime;
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

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.01f)
        {
            coyoteTimeCounter = 0.0f;
        }
        #endregion

        #region Dash

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

    private void ChangeAnimation()
    {
        animator.SetBool("Running", horizontalInput != 0);
        animator.SetBool("Jumping", rb.velocity.y > 0.01f);
        animator.SetBool("Falling", rb.velocity.y < -0.01f);
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

    public void SavePlayer()
    {
        SaveData.SavePlayerData(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveData.LoadPlayerData();
        Vector3 playerPosition;
        playerPosition.x = data.playerPos[0];
        playerPosition.y = data.playerPos[1];
        playerPosition.z = data.playerPos[2];

        transform.position = playerPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGround.position, checkGroundRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
