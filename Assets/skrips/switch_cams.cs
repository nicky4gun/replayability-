using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraPoints;  // Array of camera points (for room transitions)
    public float smoothTime = 0.3f;   // Smooth transition time
    public Vector3 offset;            // Camera offset
    public Transform player;          // Reference to the player

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private bool isCameraLocked = true;  // Is the camera locked to a point?

    void Start()
    {
        if (cameraPoints.Length > 0)
        {
            targetPosition = cameraPoints[0].position + offset;  // Start at the first camera point
        }
    }

    void Update()
    {
        // If the camera is locked to a room, move smoothly to the target position
        if (isCameraLocked)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            // If not locked, follow the player freely with offset
            transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref velocity, smoothTime);
        }

        // Check for a toggle button press to switch between room-based and free movement
        if (Input.GetKeyDown(KeyCode.T)) // Change KeyCode.T to any key you prefer
        {
            ToggleCameraMode();
        }
    }

    // Move camera to a specific point
    public void MoveToPoint(int index)
    {
        if (index >= 0 && index < cameraPoints.Length)
        {
            targetPosition = cameraPoints[index].position + offset;
        }
    }

    // Toggle between room-based movement and free movement
    public void ToggleCameraMode()
    {
        isCameraLocked = !isCameraLocked; // Toggle between locked and free movement
        Debug.Log("Camera mode toggled. Locked: " + isCameraLocked);
    }

    // Optional: Add more camera points dynamically if needed
    public void AddCameraPoint(Transform newPoint)
    {
        System.Array.Resize(ref cameraPoints, cameraPoints.Length + 1);
        cameraPoints[cameraPoints.Length - 1] = newPoint;
    }
}
