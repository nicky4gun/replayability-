using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public static DeathCounter Instance;
    public int deathCount = 0;
    public TMP_Text deathText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // If deathText is missing, try to find it again
        if (deathText == null)
        {
            GameObject foundText = GameObject.Find("DeathText"); // <-- your UI object name!
            if (foundText != null)
            {
                deathText = foundText.GetComponent<TMP_Text>();
                UpdateUI();
            }
        }
    }

    public void AddDeath()
    {
        deathCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (deathText != null)
        {
            deathText.text = "Deaths: " + deathCount.ToString();
        }
    }
}
