using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Playables;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InputActionActivation : MonoBehaviour
{

    [SerializeField]
    private XRRayInteractor rightRayInteractor;
    [SerializeField]
    private XRRayInteractor leftRayInteractor;

    public PlayableDirector director;

    [SerializeField]
    private ActionBasedSnapTurnProvider turnProvider;

    [SerializeField]
    private Transform leftTeleportInteractor;
    [SerializeField]
    private Transform rightTeleportInteractor;

    private bool rightHolding;
    private bool leftHolding;

    private Narrator narrator;
    private PlaySteps playStepsScript;
    
    private void Start()
    {
        rightRayInteractor.selectEntered.AddListener(x => RightHold());
        rightRayInteractor.selectExited.AddListener(x => RightRelease());
        leftRayInteractor.selectEntered.AddListener((x) => LeftHold());
        leftRayInteractor.selectExited.AddListener((x) => LeftRelease());

        rightTeleportInteractor.gameObject.SetActive(false);
        leftTeleportInteractor.gameObject.SetActive(false);
        rightHolding = false;
        leftHolding = false;
        
        narrator = director.gameObject.GetComponent<Narrator>();
        playStepsScript = director.gameObject.GetComponent<PlaySteps>();

        if (narrator == null || playStepsScript == null)
        {
            Debug.Log("Director does not have a narrator or play steps script attached. Please Check");
        }
    }

    private void Update()
    {
        InputDevice rc = XRInputManager.Instance.GetRightController();
        InputDevice lc = XRInputManager.Instance.GetLeftController();

        bool primaryR = false, secondaryR = false, primaryL = false, secondaryL = false;

        // right controller input
        if (rc.isValid)
        {
            // determines whether to show ray interactor or teleport interactor from thumbstick
            rc.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 result);
            if (result.y > 0.5 && !rightHolding)
            {
                rightRayInteractor.gameObject.SetActive(false);
                rightTeleportInteractor.gameObject.SetActive(true);
            } else
            {
                rightRayInteractor.gameObject.SetActive(true);
                rightTeleportInteractor.gameObject.SetActive(false);
            }

            // gets the primary button (A) and secondary button (B) input
            rc.TryGetFeatureValue(CommonUsages.primaryButton, out primaryR);
            rc.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryR);

        } 

        // left controller input
        if (lc.isValid)
        {
            // determines whether to show ray interactor or teleport interactor from thumbstick
            lc.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 result);
            if (result.y > 0.5 && !leftHolding)
            {
                leftRayInteractor.gameObject.SetActive(false);
                leftTeleportInteractor.gameObject.SetActive(true);
            } else
            {
                leftRayInteractor.gameObject.SetActive(true);
                leftTeleportInteractor.gameObject.SetActive(false);
            }

            // gets the primary button (X) and secondary button (Y) input
            lc.TryGetFeatureValue(CommonUsages.primaryButton, out primaryL);
            lc.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryL);
        }

        if (playStepsScript.steps.Count > 0)
        {
            Debug.Log(playStepsScript.steps[0].hasPlayed && !playStepsScript.steps[1].hasPlayed);
        }

        // if A,B,X, or Y is pressed, will play director timeline from the latest checkpoint
        // if any controller button needs to be remapped, migrate it out of this if statement
        if (primaryR || primaryL || secondaryR || secondaryL)
        {
            narrator.PlayAtCheckPoint();
        }

    }

    private void RightHold()
    {
        DisableTurn();
        rightHolding = true;
    }
    private void RightRelease()
    {
        EnableTurn();
        rightHolding = false;
    }

    private void LeftHold()
    {
        DisableTurn();
        leftHolding = true;
    }
    private void LeftRelease()
    {
        EnableTurn();
        leftHolding = false;
    }

    private void DisableTurn()
    {
        turnProvider.enableTurnLeftRight = false;
    }

    private void EnableTurn()
    {
        turnProvider.enableTurnLeftRight = true;
    }
}
