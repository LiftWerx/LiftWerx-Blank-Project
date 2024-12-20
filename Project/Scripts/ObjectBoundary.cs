using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectBoundary : MonoBehaviour
{

    [SerializeField]
    private Transform resetTransform;
    [SerializeField]
    private InteractionLayerMask resetLayer;
    [SerializeField]
    private InteractionLayerMask grabAndPlaceLayer;

    public float resetDuration = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out CustomGrabInteractable grabInteractable);
        if (grabInteractable != null && grabInteractable.respectBoundaries )
        {
            grabInteractable.interactionLayers = resetLayer;
            StartCoroutine(ResetPosition(grabInteractable));
        }
    }

    private IEnumerator ResetPosition(CustomGrabInteractable grabInteractable)
    {
        Vector3 resetPosition = (resetTransform == null) ? grabInteractable.resetTransform : resetTransform.position;
        
        float time = 0;
        Vector3 startPosition = grabInteractable.transform.position;

        while (time < resetDuration)
        {
            grabInteractable.transform.position = Vector3.Lerp(startPosition, resetPosition, time / resetDuration);
            time += Time.deltaTime;
            yield return null;
        }
        grabInteractable.transform.position = resetPosition;

        grabInteractable.interactionLayers = grabAndPlaceLayer;

    }

}
