using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text scoreText;
    public int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)Time.time;
        scoreText.text = score.ToString();
    }
}
