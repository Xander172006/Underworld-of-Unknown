using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // -- Health --
    public int maxHealth = 3;
    private int currentHealth;

    // -- Visual Display --
    private SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    private Color originalColor;

    // -- Hit Flash Effect --
    public float hitFlashDuration = 0.2f;
    private float hitTimer = 0f;
    private bool isHit = false;


    void Start()
    {
        // -- Start with max health and original color --
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (isHit)
        {
            // -- Flash the sprite color when hit --
            hitTimer += Time.deltaTime;
            if (hitTimer >= hitFlashDuration)
            {
                spriteRenderer.color = originalColor;
                isHit = false;
            }
        }
    }

    // -- Reduce health to the amount specified and handle death --
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (spriteRenderer != null)
        {
            // -- Flash the sprite color --
            spriteRenderer.color = hitColor;
            hitTimer = 0f;
            isHit = true;
        }
        if (currentHealth <= 0)
        {
            // -- Enemy dies --
            Die();
        }
    }

    // --- Destroy the enemy game object ---
    private void Die()
    {
        Destroy(gameObject);
    }
}