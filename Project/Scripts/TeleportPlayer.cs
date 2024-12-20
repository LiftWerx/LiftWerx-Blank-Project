using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    // Declare a variable to store the target position
    public Transform targetPosition;

    // Declare a variable to store the player object
    public GameObject player;
    public GameObject controllerCollider;

    // This method is called when a collider enters the trigger zone of this object
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject == controllerCollider || other.gameObject == player)
        {
            // Teleport the player to the target position
            player.transform.position = targetPosition.transform.position;
        }
    }
}