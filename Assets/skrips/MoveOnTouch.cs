using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 moveOffset = Vector2.right * 5f;  // Direction + distance (for ping-pong)
    public float moveSpeed = 3f;
    public bool pingPong = true;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2 currentTarget;
    private bool isMoving = false;
    private Vector2 moveDirection;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveOffset;
        currentTarget = targetPosition;

        moveDirection = moveOffset.normalized; // Only used for non-pingpong
    }

    void FixedUpdate()
    {
        if (!isMoving) return;

        if (pingPong)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, moveSpeed * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
            {
                currentTarget = currentTarget == targetPosition ? startPosition : targetPosition;
            }
        }
        else
        {
            // Move forever in a straight line
            transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isMoving)
                isMoving = true;

            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
