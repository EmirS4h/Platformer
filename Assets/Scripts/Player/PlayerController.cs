using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private ParticleSystem dustParticle;
    [SerializeField] private ParticleSystem dashParticle;

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float frictionAmount;

    [Header("Player Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float bufferTime;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float coyoteTimeCounter;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float bufferTimeCounter;

    [Header("Player Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    private bool isDashing = false;
    public bool canDash = false;
    private Vector2 dashDir;

    private float amount;
    private float movement;
    private float speedDif;
    private float accelRate;
    private float targetSpeed;
    private float verticalInput;
    private float horizontalInput;

    public bool isGrounded = false;
    private bool isJumping = false;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        ChangeAnimation();

        if (horizontalInput > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            FlipCharacter();
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            bufferTimeCounter = bufferTime;
        }
        else
        {
            bufferTimeCounter -= Time.deltaTime;
        }

        if (bufferTimeCounter > 0.1f && coyoteTimeCounter > 0.1f && !isJumping)
        {
            isJumping = true;
            coyoteTimeCounter = 0.0f;
            bufferTimeCounter = 0.0f;
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            isDashing = true;
            canDash = false;
            dashDir = new Vector2(horizontalInput, verticalInput);
            dashParticle.Play();
            StartCoroutine(StopDash());
        }
    }

    private void ChangeAnimation()
    {
        // Horizontal input != 0 Running
        animator.SetBool("Running", horizontalInput != 0);
        // rb.velocity.y > 0.01f ise Jumping
        animator.SetBool("Jumping", rb.velocity.y > 0.01f);
        // rb.velocity.y < -0.01f ise Falling
        animator.SetBool("Falling", rb.velocity.y < -0.01f);
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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        dustParticle.Play();
    }
    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }
    private void ApplyGravity()
    {
        // Short jump
        if (rb.velocity.y > 0.01f && !Input.GetButton("Jump"))
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
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
