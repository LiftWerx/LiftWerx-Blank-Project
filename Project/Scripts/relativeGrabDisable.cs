using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class relativeGrabDisable : MonoBehaviour
    {
        public Transform objectB; // Reference to Object B
        public float maxDistance = 5f; // Maximum distance allowed to grab the object
        //public GameObject bridleHook;

        private XRGrabInteractable interactableObject;

        void Start()
        {
            //XRGrabInteractable interactableObject = bridleHook.GetComponent<XRGrabInteractable>();
            interactableObject = GetComponent<XRGrabInteractable>();

            if (objectB == null)
            {
                Debug.LogError("Object B reference not set in the inspector!");
            }
        }

        void Update()
        {
            if (interactableObject.isSelected)
            {
                // Check the distance between this object and Object B
                float distance = Vector3.Distance(transform.position, objectB.position);

                // If the distance exceeds the maximum allowed, release the object
                if (distance > maxDistance)
                {
                    interactableObject.enabled = false; // Deselect the object
                }
            }
            else 
            {
            
                    interactableObject.enabled = true; // Turn XRGrabInteractable back on when chain is dropped back in place
            
            }
        }
    }
}