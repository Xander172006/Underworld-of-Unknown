using UnityEngine;

public class NinjaEnemyScript : MonoBehaviour
{
    // -- Visual Display of the Enemy --
    public Sprite standingSprite;
    public Sprite walkingSprite1;
    public Sprite walkingSprite2;
    public Sprite attackPrepairSprite;
    public Sprite attackStabbingSprite;

    // -- Stats and Behavior --
    public float walkSpeed = 3f;
    public float detectionRange = 8f;
    public float attackRange = 1.5f;
    public float attackPrepairTime = 0.3f;
    public float attackStabTime = 0.2f;
    public float walkAnimInterval = 0.25f;

    // -- Components and State --
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // -- Animation State --
    private float walkAnimTimer = 0f;
    private int walkSpriteIndex = 0;
    private Sprite[] walkSprites;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private bool facingRight = true;

    // -- Patrol State when not agrowth -- 
    private float patrolTimer = 0f;
    public float patrolTurnInterval = 3f; 
    public float patrolSpeed = 1f;     
    private int patrolDirection = 1;

    // -- Damage check --
    private bool hasDealtDamage = false; 

    void Start()
    {
        // -- Setup Enemy and check to find player --
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        walkSprites = new Sprite[] { walkingSprite1, walkingSprite2 };
    }

    void Update()
    {
        // -- If player is not found or dead than not interact with player --
        if (player == null || !player.gameObject.activeInHierarchy)
        return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // -- Face the player if in detection range --
        if (distanceToPlayer <= detectionRange)
        {
            if (player.position.x > transform.position.x && !facingRight)
                Flip();
            else if (player.position.x < transform.position.x && facingRight)
                Flip();
        }

        // -- Handle attack animation and damage logic --
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer < attackPrepairTime)
            {
                // -- show prepair animation --
                spriteRenderer.sprite = attackPrepairSprite;
            }
            else if (attackTimer < attackPrepairTime + attackStabTime)
            {
                // -- show stabbing animation --
                spriteRenderer.sprite = attackStabbingSprite;

                if (!hasDealtDamage)
                {
                    // -- Check if player is within attack range --
                    float dist = Vector2.Distance(transform.position, player.position);
                    if (dist <= attackRange)
                    {
                        var playerHealth = player.GetComponent<PlayerHealthManagerController>();

                        // -- Lower player health by 1 if within range --
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
                // -- Reset attack state after attack animation is done --
                isAttacking = false;
                attackTimer = 0f;
                hasDealtDamage = false;
            }
            rb.velocity = Vector2.zero;
            return;
        }

        // -- Handle movement and animations based on distance to player --
        if (distanceToPlayer <= attackRange)
        {
            isAttacking = true;
            attackTimer = 0f;
            rb.velocity = Vector2.zero;
            return;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // -- Set patrol state--
            float direction = player.position.x > transform.position.x ? 1f : -1f;
            rb.velocity = new Vector2(direction * walkSpeed, rb.velocity.y);

            // -- Walking animation --
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
            // -- Patrol behavior --
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolTurnInterval)
            {
                // -- randomly change direction every few seconds --
                patrolDirection *= -1;
                Flip();
                patrolTimer = 0f;
            }
            rb.velocity = new Vector2(patrolDirection * patrolSpeed, rb.velocity.y);

            // -- Walking animation --
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
        // -- FLip the enemy sprite --
        facingRight = !facingRight;
        spriteRenderer.flipX = facingRight;
    }
}