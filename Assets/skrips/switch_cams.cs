using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraPoints;  // Points the camera can move to
    public float smoothTime = 0.3f;   // Smooth transition speed
    public Vector3 offset;            // Optional offset
    public Transform player;          // Player reference
    public float fixedZ = -10f;       // Keep camera Z fixed

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private bool isCameraLocked = true;

    void Start()
    {
        if (cameraPoints.Length > 0)
        {
            targetPosition = cameraPoints[0].position + offset;
            initialPosition = targetPosition;
            targetPosition.z = fixedZ;
        }
    }

    void Update()
    {
        if (isCameraLocked)
        {
            targetPosition.z = fixedZ;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isCameraLocked = false; // Stop locking once we reach the point
            }
        }
        else
        {
            if (player != null)
            {
                Vector3 playerPosition = player.position;
                Vector3 cameraOffset = playerPosition - initialPosition;
                cameraOffset.z = fixedZ;

                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    initialPosition + cameraOffset + offset,
                    ref velocity,
                    smoothTime
                );
            }
        }
    }

    // Move camera to a specific point (with optional instant snap)
    public void MoveToPoint(int index, bool instant = false)
    {
        if (index >= 0 && index < cameraPoints.Length)
        {
            targetPosition = cameraPoints[index].position + offset;
            targetPosition.z = fixedZ;
            initialPosition = targetPosition;
            isCameraLocked = true;

            if (instant)
            {
                transform.position = targetPosition;
            }
        }
        else
        {
            Debug.LogWarning("Invalid camera point index: " + index);
        }
    }

    // Manually toggle camera lock
    public void SetFreeMovement(bool isFree)
    {
        isCameraLocked = !isFree;
        Debug.Log("Camera mode toggled. Locked: " + isCameraLocked);
    }

    public bool IsCameraLocked()
    {
        return isCameraLocked;
    }
}
