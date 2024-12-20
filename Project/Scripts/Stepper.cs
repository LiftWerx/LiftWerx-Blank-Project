using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stepper : MonoBehaviour
{
    [SerializeField]
    private bool canStep = false;

    public void TryNextStep()
    {
        if (canStep)
        {
            PlaySteps.Instance.PlayNextStep();
            Debug.Log("Trying Next Step");
            canStep = false;
        }
        
    }

    public void AllowStep()
    {
        canStep = true;
    }

    

}
