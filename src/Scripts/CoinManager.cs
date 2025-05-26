using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add to coin counter
            CoinCounter.Instance.AddCoin();
            // Remove coin from scene
            Destroy(gameObject);
        }
    }
}