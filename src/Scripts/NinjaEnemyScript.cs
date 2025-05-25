using UnityEngine;

public class NinjaEnemyScript : MonoBehaviour
{
    public Sprite standingSprite;
    public Sprite walkingSprite1;
    public Sprite walkingSprite2;
    public Sprite attackPrepairSprite;
    public Sprite attackStabbingSprite;

    public float walkSpeed = 3f;
    public float detectionRange = 8f;
    public float attackRange = 1.5f;
    public float attackPrepairTime = 0.3f;
    public float attackStabTime = 0.2f;
    public float walkAnimInterval = 0.25f;

    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private float walkAnimTimer = 0f;
    private int walkSpriteIndex = 0;
    private Sprite[] walkSprites;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private bool facingRight = true;

    private float patrolTimer = 0f;
    public float patrolTurnInterval = 3f; // How often to turn around when patrolling
    public float patrolSpeed = 1f;        // Slower speed when patrolling
    private int patrolDirection = 1;      // 1 = right, -1 = left

    private bool hasDealtDamage = false; // NEW: ensures player is only damaged once per attack

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        walkSprites = new Sprite[] { walkingSprite1, walkingSprite2 };
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Face the player if in detection range
        if (distanceToPlayer <= detectionRange)
        {
            if (player.position.x > transform.position.x && !facingRight)
                Flip();
            else if (player.position.x < transform.position.x && facingRight)
                Flip();
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer < attackPrepairTime)
            {
                spriteRenderer.sprite = attackPrepairSprite;
            }
            else if (attackTimer < attackPrepairTime + attackStabTime)
            {
                spriteRenderer.sprite = attackStabbingSprite;
                // Deal damage to player if in range and not already hit this attack
                if (!hasDealtDamage)
                {
                    float dist = Vector2.Distance(transform.position, player.position);
                    if (dist <= attackRange)
                    {
                        var playerHealth = player.GetComponent<PlayerHealthManagerController>();
                        if (playerHealth != null)
                        {
                            playerHealth.TakeDamage(1);
                            hasDealtDamage = true;
                        }
                    }
                }
            }
            else
            {
                isAttacking = false;
                attackTimer = 0f;
                hasDealtDamage = false; // Reset for next attack
            }
            rb.velocity = Vector2.zero;
            return;
        }

        if (distanceToPlayer <= attackRange)
        {
            Debug.Log("Enemy attacking!");
            isAttacking = true;
            attackTimer = 0f;
            rb.velocity = Vector2.zero;
            return;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Approach player at normal walk speed
            float direction = player.position.x > transform.position.x ? 1f : -1f;
            rb.velocity = new Vector2(direction * walkSpeed, rb.velocity.y);

            // Walking animation
            walkAnimTimer += Time.deltaTime;
            if (walkAnimTimer >= walkAnimInterval)
            {
                walkSpriteIndex = (walkSpriteIndex + 1) % walkSprites.Length;
                walkAnimTimer = 0f;
            }
            spriteRenderer.sprite = walkSprites[walkSpriteIndex];
        }
        else
        {
            // Patrol left and right
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolTurnInterval)
            {
                patrolDirection *= -1;
                Flip();
                patrolTimer = 0f;
            }
            rb.velocity = new Vector2(patrolDirection * patrolSpeed, rb.velocity.y);

            // Walking animation
            walkAnimTimer += Time.deltaTime;
            if (walkAnimTimer >= walkAnimInterval)
            {
                walkSpriteIndex = (walkSpriteIndex + 1) % walkSprites.Length;
                walkAnimTimer = 0f;
            }
            spriteRenderer.sprite = walkSprites[walkSpriteIndex];
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = facingRight; // FlipX true when facing right
    }
}