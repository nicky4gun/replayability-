using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraPoints;
    public float smoothTime = 0.3f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;

    void Start()
    {
        if (cameraPoints.Length > 0)
        {
            targetPosition = cameraPoints[0].position + offset;
        }
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void MoveToPoint(int index)
    {
        if (index >= 0 && index < cameraPoints.Length)
        {
            targetPosition = cameraPoints[index].position + offset;
        }
    }
}
