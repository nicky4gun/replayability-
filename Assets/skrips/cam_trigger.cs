using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraMover camMover;
    [SerializeField] private bool isReversed = false; // Inspector toggle

    private Vector2 lastPlayerPosition; // Stores last known player position

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Transform player = other.transform;
            Vector2 playerDirection = (Vector2)player.position - lastPlayerPosition;

            // Only trigger camera movement if the player has moved sufficiently
            if (playerDirection.magnitude > 0.1f)  // Adjust this threshold as needed
            {
                if (!isReversed)
                {
                    // Default Mode: Left/Up = Forward, Right/Down = Backward
                    if (playerDirection.x < 0 || playerDirection.y > 0)
                    {
                        camMover.MoveToNextPoint();
                    }
                    else if (playerDirection.x > 0 || playerDirection.y < 0)
                    {
                        camMover.MoveBackOnePoint();
                    }
                }
                else
                {
                    // Reversed Mode: Right/Down = Forward, Left/Up = Backward
                    if (playerDirection.x > 0 || playerDirection.y < 0)
                    {
                        camMover.MoveToNextPoint();
                    }
                    else if (playerDirection.x < 0 || playerDirection.y > 0)
                    {
                        camMover.MoveBackOnePoint();
                    }
                }
            }

            lastPlayerPosition = player.position; // Update last known position after action
        }
    }
}
