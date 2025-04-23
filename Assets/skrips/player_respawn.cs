using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint; // Current active checkpoint
    private Vector3 defaultRespawn;

    [Header("References")]
    public CameraMover camMover; // Camera movement script
    public int respawnCameraIndex = 0; // Camera index to reset to

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultRespawn = respawnPoint.position;
    }

    public void Respawn()
    {
        // Move player to the respawn point
        transform.position = respawnPoint != null ? respawnPoint.position : defaultRespawn;

        // Reset velocity
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Reset camera position if available
        if (camMover != null)
        {
            camMover.MoveToPoint(respawnCameraIndex); // Move camera to specific point
        }

        Debug.Log("Player respawned!");
    }

    // Optional: Allows updating the respawn point externally (e.g., by a checkpoint)
    public void SetRespawnPoint(Transform newPoint, int newCameraIndex)
    {
        respawnPoint = newPoint;
        respawnCameraIndex = newCameraIndex;
    }
}
