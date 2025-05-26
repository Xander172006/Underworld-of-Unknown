using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManagerController : MonoBehaviour
{
    // -- Set up max health, current health, hit color, and hit flash duration --
    public int maxHealth = 3;
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    public Color hitColor = Color.red;
    public float hitFlashDuration = 0.2f;
    private Color originalColor;
    private float hitTimer = 0f;
    private bool isHit = false;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        // -- Set health to max and render in original color --
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // -- FLash color when hit --
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
        // -- Reduce health by the amount specified and handle death --
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Player took damage! Health: " + currentHealth);

        // -- Flash color --
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hitColor;
            hitTimer = 0f;
            isHit = true;
        }

        // -- Player dies if health is 0 --
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(false);
    }
}
