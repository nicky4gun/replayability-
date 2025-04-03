using UnityEngine;

public class CircularPlatform : MonoBehaviour
{
    public Transform centerPoint; // The point the platform orbits around
    public float radius = 3f; // Distance from the center
    public float speed = 2f; // Speed of movement

    private float angle = 0f; // Track rotation angle

    void Update()
    {
        angle += speed * Time.deltaTime; // Increase angle over time
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius; // X position
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius; // Y position
        transform.position = new Vector2(x, y); // Move platform
    }
}
