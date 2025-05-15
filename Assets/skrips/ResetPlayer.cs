using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Transform spawnPoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (spawnPoint != null && other.CompareTag("Player")) {
            other.transform.position = spawnPoint.position; 
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (spawnPoint != null && other.CompareTag("Player"))
        {
            other.transform.position = spawnPoint.position;
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        }
    }
}
