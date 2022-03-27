using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float gravityScale;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float frictionAmount;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;

    private float movement;
    private float speedDif;
    private float accelRate;
    private float targetSpeed;
    private float horizontalInput;
    private float lastJumpTime = 0.3f;
    private float lastGroundedTime;


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
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        ChangeAnimation();

        if (horizontalInput > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            FlipCharacter();
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            isJumping = true;
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
    }

    private void ApplyGravity()
    {
        if (rb.velocity.y > 0.01f && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else if (rb.velocity.y < -0.01f)
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
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    // bu burda dursun belki lazým olur

    //private bool IsGrounded()
    //{
    //    bool grounded = Physics2D.BoxCast(bxcollider.bounds.center, bxcollider.size, 0.0f, Vector2.down, 0.1f, groundLayer);
    //    if (grounded)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
