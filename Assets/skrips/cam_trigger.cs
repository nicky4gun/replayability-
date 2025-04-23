using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraMover camMover;
    public int forwardIndex;  // Camera point when moving forward
    public int backwardIndex; // Camera point when coming back
    [SerializeField] private bool isReversed = false; // Optional for platformers that scroll in reverse

    private bool hasTriggered = false;
    private Vector2 lastPlayerPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || hasTriggered) return;

        Vector2 currentPosition = other.transform.position;
        Vector2 direction = currentPosition - lastPlayerPosition;

        if (direction.magnitude < 0.1f) return;

        int targetIndex = forwardIndex;

        if (!isReversed)
        {
            if (direction.x > 0 || direction.y < 0) // Going right or down
                targetIndex = forwardIndex;
            else
                targetIndex = backwardIndex;
        }
        else
        {
            if (direction.x < 0 || direction.y > 0) // Going left or up
                targetIndex = forwardIndex;
            else
                targetIndex = backwardIndex;
        }

        camMover.MoveToPoint(targetIndex);
        hasTriggered = true;
        lastPlayerPosition = currentPosition;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasTriggered = false;
            lastPlayerPosition = other.transform.position;
        }
    }
}
