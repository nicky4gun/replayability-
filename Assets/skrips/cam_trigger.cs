using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraMover camMover;
    [SerializeField] private bool isReversed = false;
    public int forwardIndex = 0;
    public int backwardIndex = 0;

    private Vector2 lastPlayerPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Transform player = other.transform;
            Vector2 playerDirection = (Vector2)player.position - lastPlayerPosition;

            bool movingForward = !isReversed
                ? (playerDirection.x < 0 || playerDirection.y > 0)
                : (playerDirection.x > 0 || playerDirection.y < 0);

            bool movingBackward = !isReversed
                ? (playerDirection.x > 0 || playerDirection.y < 0)
                : (playerDirection.x < 0 || playerDirection.y > 0);

            if (movingForward)
            {
                camMover.MoveToPoint(forwardIndex);
            }
            else if (movingBackward)
            {
                camMover.MoveToPoint(backwardIndex);
            }

            lastPlayerPosition = player.position;
        }
    }

    //  Call this from PlayerRespawn to reset trigger logic
    public void ResetPlayerTracking(Vector2 newPosition)
    {
        lastPlayerPosition = newPosition;
    }
}
