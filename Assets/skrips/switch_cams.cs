using UnityEngine;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraPoints; // List of predefined camera positions
    private int currentPointIndex = 0; // Tracks the current camera position index
    public float smoothSpeed = 0.125f; // Speed of camera movement
    public Vector3 offset; // Optional camera offset

    // Smooth Damp variables for camera movement
    private Vector3 currentVelocity = Vector3.zero;
    private float smoothTime = 0.3f; // Smooth transition time

    void Update()
    {
        // Smoothly move the camera to the target position
        Vector3 targetPosition = cameraPoints[currentPointIndex].position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
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

    // Reset the camera to the first camera point (index 0)
    public void ResetToFirstCameraPoint()
    {
        currentPointIndex = 0; // Reset to the first point in the array
        transform.position = cameraPoints[0].position + offset; // Set the camera to the first point
    }
}
