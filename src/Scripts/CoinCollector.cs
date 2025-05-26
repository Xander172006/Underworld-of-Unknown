using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    // --- Coincollector instance to manage coin collection and display ---
    public static CoinCounter Instance { get; private set; }
    public int coinCount = 0;
    public TextMeshProUGUI coinText; 


    // --- create new instance when game starts
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // --- update coin text along with the coin count ---
    private void Start()
    {
        UpdateCoinText();
    }

    // --- Increase coin count and update the display ---
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinText();
    }

    // --- Update text display ---
    void UpdateCoinText()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
    }
}