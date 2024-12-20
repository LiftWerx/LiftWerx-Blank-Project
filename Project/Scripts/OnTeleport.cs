using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class OnTeleport : MonoBehaviour
{

    private TeleportationAnchor teleportationAnchor;
    public UnityEvent Event;

    void Start()
    {
        teleportationAnchor = GetComponent<TeleportationAnchor>();
        teleportationAnchor.teleporting.AddListener(x => Event.Invoke());
    }


    
}
