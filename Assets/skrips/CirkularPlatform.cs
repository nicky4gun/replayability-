using UnityEngine;

public class CircularPlatform : MonoBehaviour
{
    public Transform centerPoint;
    public float radius = 3f;
    public float speed = 2f;

    private float angle = 0f;

    void Update()
    {
        angle += speed * Time.deltaTime;
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector2(x, y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Stick to the platform
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); // Unstick
        }
    }
}
