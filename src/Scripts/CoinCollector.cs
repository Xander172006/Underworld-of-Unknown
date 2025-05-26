using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Or using UnityEngine.UI if using legacy Text

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance { get; private set; }
    public int coinCount = 0;
    public TextMeshProUGUI coinText; // Assign in Inspector

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
    }
}