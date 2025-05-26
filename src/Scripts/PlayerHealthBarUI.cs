using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    // -- Set Healthbar and stages of health --
    public Image healthBarImage;
    public Sprite[] healthBarStages;

    private PlayerHealthManagerController playerHealth;

    void Start()
    {
        // -- Call to playerHealthManagerController to get current health --
        playerHealth = FindObjectOfType<PlayerHealthManagerController>();
        UpdateHealthBar();
    }

    void Update()
    {
        // update health bar during gameplay
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (playerHealth == null || healthBarStages.Length == 0) return;

        // -- Clamp the health value to the range of available health bar stages --
        int health = Mathf.Clamp(playerHealth.CurrentHealth, 0, healthBarStages.Length - 1);
        healthBarImage.sprite = healthBarStages[health];
        Debug.Log("Setting health bar sprite to: " + healthBarStages[health].name);
    }
    
}