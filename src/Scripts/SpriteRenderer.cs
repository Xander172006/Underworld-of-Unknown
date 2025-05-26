using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    // --- moving sprites ---
    public Sprite PlayerStanding;
    public Sprite PlayerWalking1;
    public Sprite PlayerWalking2; 
    public Sprite PlayerWalking3;
    public Sprite jumpSprite;
    public Sprite sprintSprite;

    // -- combat sprites ---
    public Sprite PlayerStrike;
    public Sprite PlayerSlash;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    public float strikeDuration = 0.15f;
    public float slashDuration = 0.15f; 


    // -- switching sprites ---
    private Sprite[] walkSprites;

    // --- animation variables ---
    private int walkSpriteIndex = 0;
    private float walkAnimTimer = 0f;
    public float walkAnimInterval = 0.2f; 
    
    private bool useFirstWalkSprite = true;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // --- movement values ---
    public float speed = 40f;
    public float jumpForce = 5f;
    private bool isGrounded = true;
    public bool isSprinting = false; 

    // --- use player controller ---
    private PlayerController playerController;

    void Start()
    {
        // --- Initialize components and variables ---
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();

        // -- set freeze so to not rotate sprites --
        rb.freezeRotation = true;

        // -- set walking sprites ---
        walkSprites = new Sprite[] { PlayerWalking1, PlayerWalking2, PlayerWalking3 };
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isSprinting = playerController != null && playerController.IsSprinting;

        // --- Flip sprite on direction ---
        if (horizontalInput > 0.1f)
            spriteRenderer.flipX = false;
        else if (horizontalInput < -0.1f)
            spriteRenderer.flipX = true;

        
         // --- Attack logic ---
        if (!isAttacking && Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            attackTimer = 0f;
        }

        // --- Attack animation logic ---
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer < strikeDuration)
            {
                spriteRenderer.sprite = PlayerStrike;
            }
            else if (attackTimer < strikeDuration + slashDuration)
            {
                spriteRenderer.sprite = PlayerSlash;
            }
            else
            {
                isAttacking = false;
                attackTimer = 0f;
            }
            return; 
        }

        // --- Jumping state ---
        if (!isGrounded)
        {
            spriteRenderer.sprite = jumpSprite;
        }
        // --- sprint state ---
        else if (isSprinting && Mathf.Abs(horizontalInput) > 0.1f)
        {
            spriteRenderer.sprite = sprintSprite;
        }
        // --- Walking state ---
        else if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            walkAnimTimer += Time.deltaTime;
            if (walkAnimTimer >= walkAnimInterval)
            {
                walkSpriteIndex = (walkSpriteIndex + 1) % walkSprites.Length;
                walkAnimTimer = 0f;
            }
            spriteRenderer.sprite = walkSprites[walkSpriteIndex];
        }
        // --- standing state ---
        else
        {
            spriteRenderer.sprite = PlayerStanding;
            walkAnimTimer = 0f; 
            useFirstWalkSprite = true;
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