using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    public float hitFlashDuration = 0.2f;
    private Color originalColor;
    private float hitTimer = 0f;
    private bool isHit = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (isHit)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= hitFlashDuration)
            {
                spriteRenderer.color = originalColor;
                isHit = false;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hitColor;
            hitTimer = 0f;
            isHit = true;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}