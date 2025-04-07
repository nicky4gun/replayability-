using UnityEngine;
using System.Collections;

public class Thwomp : MonoBehaviour
{
    public float dropSpeed = 10f;
    public float riseSpeed = 5f;
    public float waitTime = 1f;
    public float killVelocityThreshold = 5f;

    private Vector2 startPosition;
    private Rigidbody2D rb;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
        startPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFalling && other.CompareTag("Player"))
        {
            isFalling = true;
            rb.gravityScale = 1; // Turn on gravity to drop
            rb.linearVelocity = Vector2.down * dropSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Crush the player if falling fast
        if (collision.gameObject.CompareTag("Player") && rb.linearVelocity.y <= -killVelocityThreshold)
        {
            Debug.Log("Player crushed!");
            PlayerRespawn respawn = collision.gameObject.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }
        }

        // Hit ground � stop falling and reset after delay
        if (isFalling && collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            isFalling = false;
            StartCoroutine(RiseBackUp());
        }
    }

    IEnumerator RiseBackUp()
    {
        yield return new WaitForSeconds(waitTime);
        while (Vector2.Distance(transform.position, startPosition) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = startPosition; // Snap to exact position
    }
}
