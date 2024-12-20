using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRotor : MonoBehaviour
{
    public float rotationSpeed = 50f;
    void Update()
    {
        // Rotate the object around its local Y-axis
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
