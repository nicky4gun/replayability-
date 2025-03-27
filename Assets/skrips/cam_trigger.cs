using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    // Reference to the CameraMover script.
    public CameraMover camMover;

    // Detect trigger events
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            camMover.MoveToNextPoint();
        }
    }
}
