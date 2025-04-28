using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinManager coinManager;  // Reference to the CoinManager

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // If the player collides with the coin
        {
            // Increase the coin count in the CoinManager
            coinManager.CollectCoin();
            Destroy(gameObject);  // Destroy the coin after being collected
        }
    }
}
