using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraPoints; // List of predefined camera positions
    private int currentPointIndex = 0; // Tracks the current camera position index
    public float smoothSpeed = 0.125f; // Speed of camera movement
    public Vector3 offset; // Optional camera offset

    void Update()
    {
        // Smoothly move the camera to the target position
        Vector3 targetPosition = cameraPoints[currentPointIndex].position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }

    // Move forward to the next camera point
    public void MoveToNextPoint()
    {
        if (currentPointIndex < cameraPoints.Length - 1)
        {
            currentPointIndex++;
        }
    }

    // Move back to the previous camera point
    public void MoveBackOnePoint()
    {
        if (currentPointIndex > 0)
        {
            currentPointIndex--;
        }
    }
}
