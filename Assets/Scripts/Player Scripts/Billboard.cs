using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;

    void Start()
    {
        // Find and store a reference to the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Make sure the camera exists to avoid errors in scenes without a main camera
        if (mainCamera != null)
        {
            // Adjust the rotation of the GameObject to face the camera
            // This effectively makes the text always face towards the camera
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
    }
}
