using UnityEngine;
using System.Collections;

public class Thwomp : MonoBehaviour
{
    public float dropSpeed = 10f; // Speed when falling
    public float riseSpeed = 5f; // Speed when going back up
    public float waitTime = 1f; // Time to wait before going back up

    private Vector2 startPosition;
    private bool isFalling = false;
    private bool isReturning = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // No gravity effect
        startPosition = transform.position; // Store the original position
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFalling && other.CompareTag("Player")) // Start falling when player enters
        {
            isFalling = true;
            rb.linearVelocity = Vector2.down * dropSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling && collision.gameObject.CompareTag("Ground")) // Stop when hitting ground
        {
            rb.linearVelocity = Vector2.zero;
            isFalling = false;
            StartCoroutine(ReturnToStart());
        }
    }

    IEnumerator ReturnToStart()
    {
        yield return new WaitForSeconds(waitTime); // Wait before moving back up
        isReturning = true;
        while (Vector2.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }
        isReturning = false;
    }
}
