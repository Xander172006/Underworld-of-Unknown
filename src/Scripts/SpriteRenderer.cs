using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite walkSprite;
    public Sprite jumpSprite;
    public Sprite sprintSprite; // Add this in the Inspector

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public float speed = 40f;
    public float jumpForce = 5f;
    private bool isGrounded = true;

    public bool isSprinting = false; 

    // Reference to PlayerController to check sprinting state
    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Sprinting state from PlayerController
        bool isSprinting = playerController != null && playerController.IsSprinting;


        if (!isGrounded)
        {
            spriteRenderer.sprite = jumpSprite;
        }
        else if (isSprinting && Mathf.Abs(horizontalInput) > 0.1f)
        {
            spriteRenderer.sprite = sprintSprite;
        }
        else if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            spriteRenderer.sprite = walkSprite;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
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