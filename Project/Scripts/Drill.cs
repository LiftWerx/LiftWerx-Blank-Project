using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Drill : MonoBehaviour
{
    private InputDevice rc;
    public GameObject drillBit;
    public float rotationSpeed;
    public Transform rightHandLineOrigin;
    private Ray ray;
    private float triggerValue;
    bool isDrilling;
    void Start()
    {
    }
    private void Update()
    {
        rc = XRInputManager.Instance.GetRightController();
        if (rc.isValid)
        {
            rc.TryGetFeatureValue(CommonUsages.trigger, out float val);
            triggerValue = val;
            if (triggerValue > 0.5f)
            {
                isDrilling = true;
                drillBit.transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime * triggerValue));
            }
            else
            {
                isDrilling = false;
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (isDrilling && Physics.Raycast(rightHandLineOrigin.position, rightHandLineOrigin.TransformDirection(Vector3.forward), out hit, 100f))
        {
            if (hit.transform.TryGetComponent<Screw>(out Screw screw) && screw.canTighten)
            {
                Debug.DrawRay(rightHandLineOrigin.position,
                    rightHandLineOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                screw.Tighten();
                rc.SendHapticImpulse(0, 0.5f, 0.2f);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        }
    }
}
