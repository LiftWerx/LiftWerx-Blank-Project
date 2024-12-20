using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabInteractable : XRGrabInteractable
{

    public bool playerCanGrab;
    public CustomGrabTransformer transformer;
    public bool respectBoundaries = true;

    [SerializeField]
    private InteractionLayerMask interactionLayersForReset;
    
    public Vector3 resetTransform;

    private void Start()
    {
        resetTransform = transform.position; // try transform.localPosition if need relative to parent. (0,0,0) usually
        Debug.Log("Object " + transform.name +" reset position is " + resetTransform.ToString());
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        if (!playerCanGrab && GameManager.IsPlayer(interactor.transform)) return false;
        return base.IsSelectableBy(interactor);
    }

    public void ToggleGrabbable(bool grabbable)
    {
        Debug.Log("Set" + gameObject.name + " grabbable to " + grabbable.ToString());
        playerCanGrab = grabbable;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        transformer.SetCurrentInteractor((XRBaseInteractor)args.interactorObject);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        transformer.SetCurrentInteractor(null);
    }

    public void SetRespectBoundaries(bool respects)
    {
        respectBoundaries = respects;
    }

    public void ResetInteractionLayers()
    {
        interactionLayers = interactionLayersForReset;
    }

}
