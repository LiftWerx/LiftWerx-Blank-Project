using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFader : MonoBehaviour
{
    public Color startColor = Color.white;
    public Color endColor = Color.black;
    public float fadeDuration = 2f;
    public float startColorDuration = 1f; // Duration to stay at the start color before fading to the end color

    private Material material;
    private float timer = 0f;
    private bool fadingToStartColor = false;

    private void Start()
    {
        // Get the material of the object
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("No Renderer component found!");
            enabled = false; // Disable the script if no renderer is found
        }
    }

    private void Update()
    {
        if (!fadingToStartColor)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if we have reached the end of the start color duration
            if (timer >= startColorDuration)
            {
                fadingToStartColor = true; // Start fading to the start color
                timer = 0f; // Reset the timer
            }
            else
            {
                // Interpolate the color between startColor and endColor
                Color currentColor = Color.Lerp(startColor, endColor, timer / startColorDuration);

                // Update the material's color
                material.color = currentColor;
            }
        }
        else
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Calculate the current interpolation factor (0 to 1)
            float t = Mathf.Clamp01(timer / fadeDuration);

            // Interpolate the color between endColor and startColor
            Color currentColor = Color.Lerp(endColor, startColor, t);

            // Update the material's color
            material.color = currentColor;

            // Reset the timer and start fading again if the fade duration has elapsed
            if (t >= 1f)
            {
                fadingToStartColor = false; // Start fading to the end color
                timer = 0f; // Reset the timer
            }
        }
    }
}
