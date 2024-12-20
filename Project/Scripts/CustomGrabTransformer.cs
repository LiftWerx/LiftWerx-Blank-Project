using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class CustomGrabTransformer : XRGeneralGrabTransformer
{

    private XRBaseInteractor currentInteractor;
    public bool playerCanRotate = true;
    public float rotationSpeed = 75f;

    // overrides the Transform Process to eliminate object rotation through motion control
    // manually implements the object rotation process with thumbsticks
    public override void Process(XRGrabInteractable grabInteractable, XRInteractionUpdateOrder.UpdatePhase updatePhase, ref Pose targetPose, ref Vector3 localScale)
    {
        base.Process(grabInteractable, updatePhase, ref targetPose, ref localScale);

        if (playerCanRotate && currentInteractor != null)
        {

            int direction = 0;
            InputDevice rc = XRInputManager.Instance.GetRightController();
            InputDevice lc = XRInputManager.Instance.GetLeftController();

            if (currentInteractor.CompareTag("Right Controller") && rc.isValid)
            {

                rc.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 result);
                if (result.x > 0.5) direction = 1;
                else if (result.x < -0.5) direction = -1;

            }
            else if (currentInteractor.CompareTag("Left Controller") && lc.isValid)
            {
                lc.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 result);
                if (result.x > 0.5) direction = 1;
                else if (result.x < -0.5) direction = -1;
            }

            Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * direction * Time.deltaTime, 0);
            grabInteractable.transform.rotation *= deltaRotation;
        } 
    }

    public void SetCurrentInteractor(XRBaseInteractor interactor)
    {
        currentInteractor = interactor; 
    }
}
