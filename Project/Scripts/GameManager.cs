using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class GameManager : MonoBehaviour
{

    // Creates a singleton
    public static GameManager Instance;
    public XRInteractionManager xrInteractionManager;

    // For teleportation anchor management
    private CustomTeleportAnchor[] anchors;
    public float hideAnchorDelay;

    // For codex managment
    private List<Transform> codexes = new();
    public static string codexObjectName = "Object Codex";

    public static string playerTag = "Player";
    public static string leftControllerTag = "Left Controller";
    public static string rightControllerTag = "Right Controller";


    // Start is called before the first frame update
    void Awake()
    {

        // creates a singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        anchors = FindObjectsOfType<CustomTeleportAnchor>();
    
        foreach (var anchor in anchors)
        {
            anchor.teleporting.AddListener(RefreshAnchorVisuals);
        }


        // all grab interactables attempt to open their codex and hide all others when activated
        foreach (CustomGrabInteractable grabInteractable in FindObjectsOfType<CustomGrabInteractable>())
        {
            Transform codex = grabInteractable.transform.parent.Find(codexObjectName);
            if (codex != null)
            {
                codexes.Add(codex);
                //grabInteractable.activated.AddListener(HideOtherCodexes);
            }
        }

        xrInteractionManager = FindObjectOfType<XRInteractionManager>();
    }

    private void RefreshAnchorVisuals(TeleportingEventArgs args)
    {
        StartCoroutine(RefreshAnchorVisualsCoroutine(hideAnchorDelay, args));
    }

    private IEnumerator RefreshAnchorVisualsCoroutine(float delay, TeleportingEventArgs args)
    {
        yield return new WaitForSeconds(delay);
        foreach (var anchor in anchors)
        {
           if (anchor.gameObject.transform != args.interactableObject.transform)
            {
                if (anchor.willReappear) anchor.gameObject.SetActive(true);
            }
            
        }
        
        args.interactableObject.transform.gameObject.SetActive(false);
    }

    

    public void HideAllCodexes()
    {
        foreach (Transform codex in codexes)
        {
            codex.gameObject.SetActive(false);
        }
    }

    public static bool IsPlayer(Transform tr)
    {
        return tr.CompareTag(leftControllerTag) || tr.CompareTag(rightControllerTag);
    }

    public void EndScene()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
