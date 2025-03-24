using UnityEngine;
using System.Collections;
public class CameraMover : MonoBehaviour
{
    public Transform[] cameraPositions; // Assign your camera points in the Inspector
    private int currentPosIndex = 0;
    public float moveSpeed = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraTrigger"))
        {
            if (currentPosIndex < cameraPositions.Length - 1)
            {
                currentPosIndex++;
                StartCoroutine(MoveCamera(cameraPositions[currentPosIndex].position));
            }
        }
    }

    IEnumerator MoveCamera(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
