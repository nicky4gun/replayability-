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

    // STATIC variables will remember values after scene reload
    public static Vector3 savedRespawnPos;
    public static int savedCameraIndex;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // If savedRespawnPos is not empty, use it
        if (savedRespawnPos != Vector3.zero)
        {
            transform.position = savedRespawnPos;

            if (camMover != null)
                camMover.MoveToPoint(savedCameraIndex);
        }
        else if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
    }

    public void Respawn()
    {
        DeathCounter.Instance.AddDeath();

        // Save respawn position BEFORE reloading the scene
        savedRespawnPos = respawnPoint.position;
        savedCameraIndex = respawnCameraIndex;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetRespawnPoint(Transform newPoint, int newCameraIndex)
    {
        respawnPoint = newPoint;
        respawnCameraIndex = newCameraIndex;
    }
}
