using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeRepeating : MonoBehaviour
{
    public Material targetMaterial;  // Reference to the material on this GameObject
    public float interval = 1.0f;    // Time interval in seconds
    public float minAlpha = 0.0f;    // Minimum alpha value
    public float maxAlpha = 1.0f;    // Maximum alpha value

    private bool increasing = true;  // Flag to control whether alpha is increasing or decreasing

    void Start()
    {
        if (targetMaterial == null)
        {
            targetMaterial = GetComponent<Renderer>().material; // Get the material on this GameObject
        }

        // Start the InvokeRepeating method with the specified interval
        InvokeRepeating("ChangeAlpha", 0f, interval);
    }

    void ChangeAlpha()
    {
        Color color = targetMaterial.color;

        if (increasing)
        {
            color.a += 0.1f; // Adjust the step size as needed
            if (color.a >= maxAlpha)
            {
                increasing = false;
                color.a = maxAlpha;
            }
        }
        else
        {
            color.a -= 0.1f; // Adjust the step size as needed
            if (color.a <= minAlpha)
            {
                increasing = true;
                color.a = minAlpha;
            }
        }

        targetMaterial.color = color;
    }
}
