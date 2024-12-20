using UnityEngine;

public class SmoothLookAt : MonoBehaviour
{
    public Transform targetCamera; // Assign the camera here

    public Vector3 rotationOffset = Vector3.zero; // Set your desired offset

    void Update()
    {
        if (targetCamera != null)
        {
            // Calculate the rotation to face the camera
            Vector3 lookDirection = targetCamera.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            // Apply the rotation offset
            targetRotation *= Quaternion.Euler(rotationOffset);

            // Apply the rotation to the object
            transform.rotation = targetRotation;
        }
    }
}
