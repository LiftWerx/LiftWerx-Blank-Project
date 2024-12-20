using UnityEngine;

public class SocketController : MonoBehaviour
{
    public Animator animator; // Assign the Animator component in the Unity Inspector
    public string otherSocketTag = "OtherSocketTag"; // Set the tag of the other specific socket

    private bool isSocketFilled = false;
    private bool isOtherSocketFilled = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the tag of the socket you want to fill this socket.
        if (other.CompareTag("YourTag"))
        {
            isSocketFilled = true;
        }

        // Check if both this socket and the other specific socket are filled.
        if (isSocketFilled && isOtherSocketFilled)
        {
            // Trigger the animation
            animator.SetTrigger("YourAnimationTrigger"); // Replace with the actual trigger name
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the socket state if the exiting object had the tag of the socket you want to fill this socket.
        if (other.CompareTag("YourTag"))
        {
            isSocketFilled = false;
        }
    }

    // You may want to implement an OnTriggerExit method for the other specific socket as well.

    // Implement a method to set the state of the other specific socket.
    public void SetOtherSocketFilled(bool isFilled)
    {
        isOtherSocketFilled = isFilled;
    }
}