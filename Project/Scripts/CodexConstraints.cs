using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CodexConstraints : MonoBehaviour
{
    private GameObject XRInteractionSetup;
    public bool isConstraintActive = true;
    public bool isPositionContrained = true;
    // Start is called before the first frame update
    void Start()
    {
        XRInteractionSetup = GameObject.FindGameObjectWithTag("Player");
        if (isConstraintActive)
        {
            if (XRInteractionSetup != null)
            {
                ConstraintSource XRInteractionSetupContraint = new ConstraintSource
                {
                    sourceTransform = Camera.main.transform,
                    weight = 1
                };
                gameObject.GetComponent<LookAtConstraint>().AddSource(XRInteractionSetupContraint);
            }

            if (isPositionContrained)
            {
                ConstraintSource positionConstraint = new ConstraintSource
                {
                    sourceTransform = transform.parent.Find("Mesh"),
                    weight = 1
                };

                gameObject.GetComponent<PositionConstraint>().AddSource(positionConstraint);
            }
        }

    }
}
