using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectMover : MonoBehaviour
{
    public Transform targetPosition; // Set this in the Unity Inspector

    public float moveSpeed = 5f; // Adjust the speed as needed

    void Update()
    {
        // Check if the target position is set
        if (targetPosition != null)
        {
            // Move the object towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            // You can also use the following line for a smoother movement using Lerp:
            // transform.position = Vector3.Lerp(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Target position is not set!");
        }
    }
}