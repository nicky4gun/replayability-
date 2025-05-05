using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraMover camMover;
    [SerializeField] private bool isReversed = false;  // Whether the camera moves forward or backward
    public int forwardIndex = 0;
    public int backwardIndex = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Switch to free mode when the player enters this trigger area
            camMover.SetFreeMovement(true);  // Start free movement

            bool movingForward = other.transform.localScale.x > 0;  // Check if player is moving right

            if (movingForward)
            {
                camMover.MoveToPoint(forwardIndex);  // Move to the forward camera point
            }
            else
            {
                camMover.MoveToPoint(backwardIndex);  // Move to the backward camera point
            }
        }
    }
}
