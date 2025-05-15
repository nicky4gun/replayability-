using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    public static DeathCounter Instance;
    public static int deathCount = 0;
    public TMP_Text deathText; // UI Text to display death count

    public TMP_Text timer;
    public static int time = 0;

    void Awake()
    {
        // Ensure only one instance of DeathCounter exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); 
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }

    void Start()
    {/*
        // Try to find the DeathText if it's not assigned
        if (deathText == null)
        {
            GameObject foundText = GameObject.Find("Death Text");
            if (foundText != null)
            {
                deathText = foundText.GetComponent<TMP_Text>();
            }
        }
        if (timer == null)
        {
            GameObject foundTextTimer = GameObject.Find("TimerTxt");
            if (foundTextTimer != null)
            {
                timer = foundTextTimer.GetComponent<TMP_Text>();
            }
        }*/
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        time = (int)Time.time;
        timer.text = time.ToString();
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
