using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (deathText == null)
        {
            // Try to find the new TextMeshProUGUI by Tag
            GameObject deathTextObj = GameObject.FindWithTag("DeathText");

            if (deathTextObj != null)
            {
                deathText = deathTextObj.GetComponent<TMP_Text>();
            }
        }

        // Update the UI immediately after reloading
        if (deathText != null)
        {
            deathText.text = "Deaths: " + deathCount.ToString();
        }
    }

    public void AddDeath()
    {
        deathCount++;
        Debug.Log("Deaths: " + deathCount);

        if (deathText != null)
        {
            deathText.text = "Deaths: " + deathCount.ToString();
        }
    }

    public void ResetDeaths()
    {
        deathCount = 0;

        if (deathText != null)
        {
            deathText.text = "Deaths: " + deathCount.ToString();
        }
    }
}
