using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerInsideTrigger : MonoBehaviour
{
    // Define a UnityEvent to hold the actions that will be triggered
    [System.Serializable]
    public class TriggerAction
    {
        public UnityEvent actionEvent; // The UnityEvent that calls methods
    }

    // List of actions to perform when the player enters the trigger zone
    public TriggerAction[] playerEnterActions;
    public TriggerAction[] playerExitActions;

    // Reference to the player's tag (default is "Player")
    public string playerTag = "Player";

    // Detect when the player enters the trigger zone
    Collider player;
    bool playerInside = false;

    private void Update()
    {
        if (player != null && playerInside)
        {
            if (!transform.GetComponent<Collider>().bounds.Contains(player.transform.position))
            {
                exitTrigger();
                playerInside = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player = other;
            playerInside = true;
            // Execute all actions in the playerEnterActions list
            foreach (var action in playerEnterActions)
            {
                action.actionEvent.Invoke(); // Calls the method assigned in the Inspector
            }
        }
    }

    private void exitTrigger()
    {
        Debug.Log("Player Exit");
        // Execute all actions in the playerEnterActions list
        foreach (var action in playerExitActions)
        {
            action.actionEvent.Invoke(); // Calls the method assigned in the Inspector
        }
    }
}