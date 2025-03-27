using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // List of camera points set in the inspector.
    public Transform[] cameraPoints;
    // Index of the current camera point.
    private int currentPointIndex = 0;
    // Smooth transition speed.
    public float smoothSpeed = 0.125f;
    // Optional offset from the target point.
    public Vector3 offset;

    void Update()
    {
        // Calculate the desired position and move smoothly.
        Vector3 desiredPosition = cameraPoints[currentPointIndex].position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    // Call this method to move to the next camera point.
    public void MoveToNextPoint()
    {
        if (currentPointIndex < cameraPoints.Length - 1)
        {
            currentPointIndex++;
        }
    }
}
