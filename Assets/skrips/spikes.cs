using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit spikes!");

            PlayerRespawn respawn = collision.gameObject.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}
