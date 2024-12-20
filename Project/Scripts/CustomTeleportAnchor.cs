using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomTeleportAnchor : TeleportationAnchor
{
    public bool willReappear = true;
    public Transform linkedPosition = null;

    private void SetAnchorTransform(Vector3 pos, Quaternion rot)
    {
        teleportAnchorTransform.SetPositionAndRotation(pos, rot);
    }

    public void SetLinkedPosition(Transform link) { linkedPosition = link; }
    public void SetWillReappear(bool reappear) {  willReappear = reappear; }

    protected override bool GenerateTeleportRequest(IXRInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
    {
        if (linkedPosition != null )
        {
            SetAnchorTransform(linkedPosition.position, linkedPosition.transform.rotation);
        }
        
        bool success = base.GenerateTeleportRequest(interactor, raycastHit, ref teleportRequest);
        
        if (success)
        {
            SetAnchorTransform(this.transform.position, this.transform.rotation);
        }

        return success;
    }
}
