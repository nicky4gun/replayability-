using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int totalCoins = 3;  // The total number of coins to collect
    private int collectedCoins = 0;  // The number of coins the player has collected

    public UIManager uiManager;  // Reference to the UIManager script

    // Method to collect a coin
    public void CollectCoin()
    {
        if (collectedCoins < totalCoins)
        {
            collectedCoins++;
            uiManager.UpdateCoinDisplay(collectedCoins, totalCoins);  // Update the UI
        }
    }

    // Get the number of collected coins
    public int GetCollectedCoins()
    {
        return collectedCoins;
    }
}
