using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- speed variables ---
    public float minSpeed = 10f;
    public float maxSpeed = 30f;
    public float sprintTimeToMax = 5f;
    public bool IsSprinting => isSprinting;

    // --- moving time variables ---
    private float moveHoldTime = 0f; 
    private float lastInputDirection = 0f;
    
    // --- jump variables ---
    public float jumpForce = 40f;
    public float fallMultiplier = 2.5f; 

    // -- ground check variables ---
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isSprinting = false; 

    // -- off screen collision check ---
    private Vector2 screenBoundsMin;
    private Vector2 screenBoundsMax;
    private float playerWidth;
    private float playerHeight;
    public float worldLeftBoundary = -10f;


    // Add these fields to PlayerController
    public float attackRange = 1.5f;
    public int attackDamage = 1;
    public LayerMask enemyLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // --- Get the screen bounds in world coordinates ---
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        screenBoundsMin = new Vector2(bottomLeft.x, bottomLeft.y);
        screenBoundsMax = new Vector2(topRight.x, topRight.y);

        // --- Find the player's width and height ---
        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            playerWidth = col.bounds.extents.x;
            playerHeight = col.bounds.extents.y;
        }
        else
        {
            playerWidth = 0.5f;
            playerHeight = 0.5f;
        }
    }

    // --- Visualize attack range in editor ---
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
    void Attack()
    {
        // --- Detect enemies in range ---
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }


    void Update()
    {
        // -- move on the horizontal axis --
        float horizontalInput = Input.GetAxis("Horizontal");

        // --- Check how long the player has been holding the input ---
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            if (Mathf.Sign(horizontalInput) != lastInputDirection)
            {
                moveHoldTime = 0f;
            }
            // --- Increase moveHoldTime and movement speed ---
            moveHoldTime += Time.deltaTime;
            lastInputDirection = Mathf.Sign(horizontalInput);
        }
        // --- Reset moveHoldTime ---
        else
        {
            moveHoldTime = 0f;
            lastInputDirection = 0f;
        }

        // --- Check if the player is sprinting ---
        moveHoldTime = Mathf.Clamp(moveHoldTime, 0f, sprintTimeToMax);

        // --- Take current speed and apply it to the player ---
        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, moveHoldTime / sprintTimeToMax);
        rb.velocity = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);
        
        // --- Attack with mouse left click ---
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // --- jump input ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * 5, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // --- Faster jumping ---
        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier + 1) * Time.deltaTime;
        }

        // --- Faster falling ---
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier + 1) * Time.deltaTime;
        }

        // --- Off screen collision check ---
        Vector3 pos = transform.position;
        pos.x = Mathf.Max(pos.x, worldLeftBoundary + playerWidth); // Only clamp to world left boundary
        pos.y = Mathf.Clamp(pos.y, screenBoundsMin.y + playerHeight, screenBoundsMax.y - playerHeight);
        transform.position = pos;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        
        // -- Collision with ground --
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // -- Collision with dangerous objects --
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Enemy"))
        {
            PlayerHealthManagerController playerHealth = GetComponent<PlayerHealthManagerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }
}