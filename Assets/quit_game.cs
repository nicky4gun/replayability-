using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Useful for testing in the Editor
        Application.Quit();
    }
}
