using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;  // Reference to the TextMeshProUGUI component for coin display

    // Method to update the coin display on the UI
    public void UpdateCoinDisplay(int collectedCoins, int totalCoins)
    {
        coinText.text = $"{collectedCoins}/{totalCoins}";  // Display coins as "collected/total"
    }
}
