using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public CameraMover camMover; // Reference to the CameraMover script

    private Vector3 defaultRespawn;

    void Start()
    {
        defaultRespawn = respawnPoint.position;
    }

    public void Respawn()
    {
        // Move player to respawn point
        transform.position = defaultRespawn;

        // Reset player velocity
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Reset the camera to the first camera point
        if (camMover != null)
        {
            camMover.ResetToFirstCameraPoint(); // Camera resets to first point
        }
    }
}
