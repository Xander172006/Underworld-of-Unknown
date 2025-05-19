using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 40f;
    public float sprintSpeed = 45f;
    public float jumpForce = 40f;
    public float fallMultiplier = 2.5f; 
    private Rigidbody2D rb;
    private bool isGrounded = true;

    private float holdTime = 0f;
    private float sprintThreshold = 0.5f;

    public bool isSprinting = false;
    public bool IsSprinting => isSprinting;
    
    private float lastInputDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Sprint logic
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            if (Mathf.Sign(horizontalInput) != lastInputDirection)
            {
                holdTime = 0f;
                isSprinting = false;
            }
            holdTime += Time.deltaTime;
            lastInputDirection = Mathf.Sign(horizontalInput);

            if (holdTime >= sprintThreshold)
            {
                isSprinting = true;
            }
        }
        else
        {
            holdTime = 0f;
            isSprinting = false;
            lastInputDirection = 0f;
        }

        float currentSpeed = isSprinting ? sprintSpeed / 3 : speed * 3;
        rb.velocity = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);


        // jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * 5, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Faster jumping
        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier + 1) * Time.deltaTime;
        }

        // Faster falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier + 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}