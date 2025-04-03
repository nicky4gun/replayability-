using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right; // Moves to the right
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // No gravity effect
        rb.linearVelocity = Vector2.zero; // Start stationary
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving && collision.gameObject.CompareTag("Player"))
        {
            // Start moving when the player lands on the platform
            isMoving = true;
            rb.linearVelocity = moveDirection.normalized * moveSpeed;
        }
    }
}


