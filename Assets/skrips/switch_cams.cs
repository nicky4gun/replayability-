using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex = 0;

    void Start()
    {
        // Disable all cameras except the first one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraTrigger"))
        {
            // Find the next camera and activate it
            cameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex++;

            if (currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex].gameObject.SetActive(true);
            }
        }
    }
}
