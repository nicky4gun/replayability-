using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int cameraIndex = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.SetRespawnPoint(transform, cameraIndex);
                Debug.Log("Checkpoint updated!");
            }
        }
    }
}
