using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene you want to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            LoadLevel(sceneToLoad);
        }
    }
    public void LoadLevel(string _sceneToLoad)
    {

        SceneManager.LoadScene(_sceneToLoad);
    }
}
