using UnityEngine;

public class SmoothScale : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 targetScale;
    private float scaleSpeed = 2.0f; // Speed of the scaling animation
    private bool isScaling = false; // Flag to track if scaling is in progress
    private bool scaled = false;
    void Start()
    {
        // Save the original scale at the start
        originalScale = transform.localScale;
        targetScale = originalScale; // Initially, target scale is the original scale
    }

    void Update()
    {
        // Smoothly interpolate between the current scale and the target scale
        if (isScaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

            // Check if the scaling has finished (within a small threshold)
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale; // Set the scale exactly to the target scale
                isScaling = false; // Stop scaling
            }
        }
    }

    // Function to scale the object to 5 times its size smoothly
    public void scaletoSize(int size)
    {
        if (!scaled)
        {
            targetScale = originalScale * size;
            isScaling = true; // Start scaling
            scaled = true;
        }
    }

    // Function to shrink the object back to its original size smoothly
    public void shrinkToOriginalSize(bool instant)
    {
        if (instant)
        {
            targetScale = originalScale;
            transform.localScale = targetScale;
        }
        else
        {
            targetScale = originalScale;
            isScaling = true; // Start shrinking
        }
        scaled = false;
    }
}
