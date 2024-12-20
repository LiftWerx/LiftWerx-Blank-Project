using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f; // Distance the object will move up and down
    public float frequency = 1f; // Speed of the up and down movement

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position
        Vector3 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new position
        transform.position = newPosition;
    }
}
