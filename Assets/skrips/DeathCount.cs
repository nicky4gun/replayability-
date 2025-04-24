using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public static DeathCounter Instance;
    public int deathCount = 0;
    public TMP_Text deathText; // Assign in the inspector if you want UI

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
        // Reset deaths only for scenes that start with "Level"
        if (scene.name.StartsWith("Level"))
        {
            ResetDeaths();
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