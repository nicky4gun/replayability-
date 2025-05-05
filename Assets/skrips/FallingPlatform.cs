using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallSpeed = 5f;   // Speed at which the platform falls
    public float waitTime = 1f;    // Time to wait at the bottom before respawning back
    private Vector3 originalPosition; // The original position of the platform
    private Rigidbody2D rb;  // Rigidbody2D for physics

    private bool isFalling = false; // To track if it's falling

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;  // Initially no gravity, we control the falling
        originalPosition = transform.position; // Store the original position of the platform
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When the player lands on the platform
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;  // Start the falling process
            rb.gravityScale = 1;  // Let gravity affect the platform
        }
    }

    void Update()
    {
        if (isFalling)
        {
            // Apply falling speed
            rb.linearVelocity = new Vector2(0, -fallSpeed);  // Fall downwards

            // After the platform has fallen, stop falling and wait
            if (transform.position.y <= originalPosition.y - 2f)  // Adjust this based on your game
            {
                rb.linearVelocity = Vector2.zero;  // Stop the fall
                StartCoroutine(RespawnPlatform()); // Start respawn process
            }
        }
    }

    IEnumerator RespawnPlatform()
    {
        yield return new WaitForSeconds(waitTime); // Wait for a short time before respawning

        // Reset platform position to original
        transform.position = originalPosition;

        // Stop platform's physics and gravity
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        // Reset falling state
        isFalling = false;
    }
}
