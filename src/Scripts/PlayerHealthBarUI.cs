using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    public Image healthBarImage;
    public Sprite[] healthBarStages;

    private PlayerHealthManagerController playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealthManagerController>();
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

  void UpdateHealthBar()
    {
        if (playerHealth == null || healthBarStages.Length == 0) return;
        int health = Mathf.Clamp(playerHealth.CurrentHealth, 0, healthBarStages.Length - 1);
        healthBarImage.sprite = healthBarStages[health];
        Debug.Log("Setting health bar sprite to: " + healthBarStages[health].name);
    }
}