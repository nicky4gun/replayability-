using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraPoints;  // Array of camera points (for room transitions)
    public float smoothTime = 0.3f;   // Smooth transition time
    public Vector3 offset;            // Camera offset relative to the player
    public Transform player;          // Reference to the player
    public float fixedZ = -10f;       // Fixed Z value for the camera

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;   // Target position the camera will move to
    private Vector3 initialPosition;  // The initial camera point (where it was set to follow)

    private bool isCameraLocked = true;  // Whether the camera is locked to a specific point

    void Start()
    {
        // Set initial position to the first camera point
        if (cameraPoints.Length > 0)
        {
            targetPosition = cameraPoints[0].position + offset;
            initialPosition = targetPosition;  // Store the initial camera position when locked
            targetPosition.z = fixedZ;         // Ensure Z is always fixed
        }
    }

    void Update()
    {
        if (isCameraLocked)
        {
            // If the camera is locked, follow the camera points smoothly
            targetPosition.z = fixedZ;  // Keep Z fixed to a set value
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // After reaching the target position, unlock the camera to follow the player
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isCameraLocked = false;  // Camera unlocked, now free to follow the player
            }
        }
        else
        {
            // Camera is in free movement mode, follow the player based on the initial position
            Vector3 playerPosition = player.position;
            Vector3 cameraOffset = playerPosition - initialPosition; // Calculate offset from initial camera point
            cameraOffset.z = fixedZ;  // Keep Z fixed to -10

            // Update camera position based on player's position and the initial camera offset
            transform.position = Vector3.SmoothDamp(transform.position, initialPosition + cameraOffset + offset, ref velocity, smoothTime);
        }
    }

    // Move camera to a specific point (teleport to the point and start following)
    public void MoveToPoint(int index)
    {
        if (index >= 0 && index < cameraPoints.Length)
        {
            targetPosition = cameraPoints[index].position + offset;
            targetPosition.z = fixedZ; // Ensure Z is always fixed to -10
            initialPosition = targetPosition; // Set the initial position as the target point
            isCameraLocked = true; // Lock the camera to this point
        }
    }

    // Toggle between locked movement and free movement
    public void SetFreeMovement(bool isFree)
    {
        if (isFree)
        {
            isCameraLocked = false;  // Disable camera lock to allow free movement
        }
        else
        {
            isCameraLocked = true;  // Lock the camera to a specific point again
        }

        Debug.Log("Camera mode toggled. Locked: " + isCameraLocked);
    }

    // Check if the camera is locked
    public bool IsCameraLocked()
    {
        return isCameraLocked;
    }
}
