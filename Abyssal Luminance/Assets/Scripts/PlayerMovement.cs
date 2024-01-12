using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 15f;
    public float deceleration = 15f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float dashSpeed = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDoubleJump;
    private float currentSpeed;
    private float lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        HandleMovement(moveX);

        // Jumping
        HandleJumping();

        // Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(currentSpeed * dashSpeed, rb.velocity.y);
        }
    }

    private void HandleMovement(float moveX)
    {
        if (moveX != 0)
        {
            // Accelerate or maintain current speed
            if (moveX != lastDirection)
            {
                currentSpeed = 0; // Reset speed when changing direction
            }

            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
            lastDirection = moveX;
        }
        else
        {
            // Decelerate
            currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);
        }

        rb.velocity = new Vector2(moveX * currentSpeed, rb.velocity.y);
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.velocity = Vector2.up * jumpForce;
                canDoubleJump = false;
            }
        }

        // Modify gravity for more realistic jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}