using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint;
    private Vector3 defaultRespawn;

    [Header("References")]
    public CameraMover camMover;
    public int respawnCameraIndex = 0;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultRespawn = respawnPoint.position;
    }

    public void Respawn()
    {
        DeathCounter.Instance.AddDeath(); // Increase death counter

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Timer and DeathCounter will survive because of DontDestroyOnLoad (make sure you have that)
    }

    public void SetRespawnPoint(Transform newPoint, int newCameraIndex)
    {
        respawnPoint = newPoint;
        respawnCameraIndex = newCameraIndex;
    }
}
