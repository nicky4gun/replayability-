using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallSpeed = 5f;   // Speed at which it falls
    public float riseSpeed = 2f;   // Speed at which it rises back
    public float waitTime = 1f;    // Time to wait at the bottom before rising back
    private Vector3 originalPosition; // The original position of the platform
    private Rigidbody2D rb;  // Rigidbody2D for physics

    private bool isFalling = false; // To track if it's falling
    private bool isRising = false;  // To track if it's rising back up

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;  // No gravity so we control the falling
        originalPosition = transform.position; // Store the original position
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When the player lands on the platform
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;  // Start the falling process
            rb.gravityScale = 1;  // Allow gravity to affect the platform
        }
    }

    void Update()
    {
        if (isFalling)
        {
            // When falling, apply a downward force
            rb.linearVelocity = new Vector2(0, -fallSpeed);  // Fall down

            // After the platform hits the ground, stop the fall and wait
            if (transform.position.y <= originalPosition.y - 2f)  // Check if it has fallen enough (adjust for your game)
            {
                rb.linearVelocity = Vector2.zero; // Stop falling
                StartCoroutine(RiseBackUp()); // Start rising
            }
        }
    }

    IEnumerator RiseBackUp()
    {
        yield return new WaitForSeconds(waitTime); // Wait for a short time before rising
        isRising = true;

        // Move the platform back to its original position
        while (transform.position.y < originalPosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }

        isRising = false;  // Once it reaches its original position, stop rising
        rb.gravityScale = 0;  // Disable gravity again to keep the platform in place
        isFalling = false;  // Reset the falling flag
    }
}
