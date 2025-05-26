using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // -- If player interacts with coin, It will be collected and removed from the scene ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinCounter.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}