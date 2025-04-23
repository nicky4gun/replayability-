using UnityEngine;
using System.Collections;
public class CameraMover : MonoBehaviour
{
    public Transform[] cameraPoints; // List of predefined camera positions
    public Vector3 offset; // Optional camera offset

    [Tooltip("Time it takes to smooth between camera points.")]
    public float smoothTime = 0.3f;

    private int currentPointIndex = 0; // Tracks the current camera position index
    private Vector3 currentVelocity = Vector3.zero; // For SmoothDamp

    private bool isTransitioning = false; // Flag to prevent multiple transitions at once
    private float transitionCooldown = 0.5f; // Cooldown time to prevent snapping

    void Update()
    {
        // Check if there are camera points and make sure we have a valid target
        if (cameraPoints.Length == 0 || cameraPoints[currentPointIndex] == null) return;

        // Smoothly move the camera to the target position
        Vector3 targetPosition = cameraPoints[currentPointIndex].position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    // Move forward to the next camera point
    public void MoveToNextPoint(bool loop = false)
    {
        if (isTransitioning) return;

        if (currentPointIndex < cameraPoints.Length - 1)
        {
            currentPointIndex++;
            StartCoroutine(TransitionCooldown());
        }
        else if (loop) // Optional looping behavior
        {
            currentPointIndex = 0;
            StartCoroutine(TransitionCooldown());
        }
    }

    // Move back to the previous camera point
    public void MoveBackOnePoint()
    {
        if (isTransitioning) return;

        if (currentPointIndex > 0)
        {
            currentPointIndex--;
            StartCoroutine(TransitionCooldown());
        }
    }

    // Reset the camera to the first camera point (index 0)
    public void ResetToFirstCameraPoint()
    {
        if (cameraPoints.Length == 0) return;

        currentPointIndex = 0;
        transform.position = cameraPoints[0].position + offset;
    }

    // Start a cooldown before transitioning again
    private IEnumerator TransitionCooldown()
    {
        isTransitioning = true;
        yield return new WaitForSeconds(transitionCooldown); // Wait for the cooldown
        isTransitioning = false;
    }

    // Optionally, add a method to instantly jump to any camera point
    public void SnapToPoint(int index)
    {
        if (index >= 0 && index < cameraPoints.Length)
        {
            currentPointIndex = index;
            transform.position = cameraPoints[index].position + offset;
        }
    }
}
