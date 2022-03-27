using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D bxcollider;
    [SerializeField] private LayerMask groundLayer;

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
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
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bxcollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        Move();
        ApplyGravity();
        ApplyFriction();
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

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //Jump();
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
        rb.AddForce(movement* Vector2.right);
    }
    // Better Jumping
    private void ApplyGravity()
    {
        if (rb.velocity.y < -0.1f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0.1f && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    private void ApplyFriction()
    {
        if (IsGrounded() && Mathf.Abs(horizontalInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }
    // Rotates the character to the right direction
    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bxcollider.bounds.center, bxcollider.size, 0.0f, Vector2.down, 0.1f, groundLayer);
    }
}
