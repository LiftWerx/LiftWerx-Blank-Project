using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointToOrigin : MonoBehaviour
{
    public Transform target;  // The target object to point at

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction from the current position to the target position
            Vector3 direction = target.position - transform.position;

            // Calculate the rotation required to look in that direction
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Apply the rotation to the object
            transform.rotation = rotation;
        }
    }
}

