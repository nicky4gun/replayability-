using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraMover camMover;
    [SerializeField] private bool isReversed = false; // For reverse movement logic
    public int forwardIndex = 0;
    public int backwardIndex = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered collider");

            // Check if the player is moving forward or backward
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            bool movingForward = playerRb.linearVelocity.x > 0;  // Moving right
            bool movingBackward = playerRb.linearVelocity.x < 0; // Moving left

            // Lock the camera to a point based on movement direction
            if (movingForward)
            {
                camMover.MoveToPoint(forwardIndex); // Move camera forward
                camMover.SetFreeMovement(false);    // Lock camera to room
            }
            else if (movingBackward)
            {
                camMover.MoveToPoint(backwardIndex); // Move camera backward
                camMover.SetFreeMovement(false);     // Lock camera to room
            }
        }
    }

    // Call this to switch to free movement when the player is outside of rooms
    public void EnableFreeMovement()
    {
        camMover.SetFreeMovement(true); // Allow the camera to follow the player freely
    }
}
