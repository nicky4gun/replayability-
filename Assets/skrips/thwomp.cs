using UnityEngine;
using System.Collections;

public class Thwomp : MonoBehaviour
{
    public float dropSpeed = 10f;
    public float riseSpeed = 5f;
    public float waitTime = 1f;
    public float killVelocityThreshold = 10f;

    private Vector2 startPosition;
    private Rigidbody2D rb;
    private bool isFalling = false;
    public Vector2 moveDirection = Vector2.down;
    public float gravityScale = 1f;
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
            Debug.Log("hit trigger");
            isFalling = true;
            rb.gravityScale = gravityScale; // Turn on gravity to drop
            //rb.AddForce(moveDirection*dropSpeed,ForceMode2D.Impulse);
            rb.linearVelocity = moveDirection * dropSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Crush the player if falling fast
        if (collision.gameObject.CompareTag("Player") && collision.relativeVelocity.magnitude >= killVelocityThreshold)
        {
            Debug.Log("Player crushed! " + collision.relativeVelocity.magnitude);
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
