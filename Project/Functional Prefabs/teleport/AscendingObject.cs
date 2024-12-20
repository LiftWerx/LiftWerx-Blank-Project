using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendingObject : MonoBehaviour
{
    public float ascendSpeed = 1f;
    public float teleportHeight = 10f;
    public Vector3 startingPosition;

    private void Start()
    {
        startingPosition = new Vector3 (transform.localPosition.x, startingPosition.y, transform.localPosition.z);
    }

    private void Update()
    {
        // Ascend the object gradually
        transform.Translate(Vector3.up * ascendSpeed * Time.deltaTime);

        // Check if the object has reached the teleport height
        if (transform.localPosition.y >= teleportHeight)
        {
            // Teleport the object to the starting position
            transform.localPosition = startingPosition;
        }
    }
}
