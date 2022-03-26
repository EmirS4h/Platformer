using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D bxcollider;
    [SerializeField] private LayerMask groundLayer;

    private float horizontalInput;
    private bool isFacingRight = true;

    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bxcollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // Horizontal input != 0 Running
        animator.SetBool("Running", horizontalInput != 0);
        // rb.velocity.y > 0.01f ise Jumping
        animator.SetBool("Jumping", rb.velocity.y > 0.01f);
        // rb.velocity.y < -0.01f ise Falling
        animator.SetBool("Falling", rb.velocity.y < -0.01f);

        if (horizontalInput > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            FlipCharacter();
        }

        Move();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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

    private void AhmedinMg()
    {
        Debug.Log("AHMEDIN MGGGG");
    }
}
