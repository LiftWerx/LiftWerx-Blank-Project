using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SecondaryCollider : MonoBehaviour
{
    public bool objectIsTouching;
    public InteractionLayerMask grabAndPlaceLayers;

    private void OnTriggerEnter(Collider other)
    {
        objectIsTouching = true;
    }

    private void OnTriggerExit(Collider other)
    {
        objectIsTouching = false;

        // a precaution for if a socket ever removes an interactable from the grab layer when the object is not snapped into the socket
        XRGrabInteractable interactable = other.transform.GetComponent<XRGrabInteractable>();
        if (interactable != null) interactable.interactionLayers = grabAndPlaceLayers;
    }
}
