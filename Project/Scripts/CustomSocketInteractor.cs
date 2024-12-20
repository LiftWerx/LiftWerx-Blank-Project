using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketInteractor : XRSocketInteractor
{
    // to constrain what object is selected + position/orientation - see hover and selection guards

    [Tooltip("Objects must have this tag to interact with the socket")]
    [SerializeField]
    public string targetTag;
    private Quaternion socketRotation;
    [Tooltip("The maximum difference in angle that the object may have in order to snap into the socket")]
    [SerializeField]
    private float rotationForgiveness = 30f;
    private SecondaryCollider[] secondaryColliders;
    private bool placed = false;

    // allows sockets to auto-select items that the player is holding
    private bool hovering = false;
    private IXRHoverInteractable hoveredObject;
    // This should be set to "Place" only, which will remove the object from the "Grab" layer
    // This ensures the socket will 'take' objects from the player without the player having to release them
    [Tooltip("The interaction layers the selected object will be set to upon selection")]
    [SerializeField]
    public InteractionLayerMask placeLayer;

    // additional components that should be triggered / modified on select
    [SerializeField]
    private bool playSoundEffect = true;
    [SerializeField]
    private bool playParticles = true;
    private AudioSource audioSource;
    private ParticleSystem particles;
    public GameObject socketMesh;
    public GameObject objectMesh;
    public bool willHideObject = false;

    public bool replaceWithOptimized;
    [SerializeField] private GameObject optimizedMesh;
    protected override void Start()
    {
        base.Start();
        socketRotation = this.transform.rotation;
        secondaryColliders = GetComponentsInChildren<SecondaryCollider>();

        Transform particlesObject = transform.Find("Particles");
        if (particlesObject != null)
        {
            particles = particlesObject.GetComponent<ParticleSystem>();

        }

        Transform audioObject = transform.Find("Audio Source");
        if (audioObject != null)
        {
            audioSource = audioObject.GetComponent<AudioSource>();
        }
        
        if(socketMesh == null) socketMesh = transform.Find("Mesh").gameObject;
    }

    // NOTE: THESE THREE LARGE CHUNKS OF COMMENTED CODE ENABLE
    // SOCKETS TO GRAB INTERACTABLES FROM THE PLAYER WITHOUT THEM
    // LETTING GO. THERE IS A BUG WHERE IF AN INTERACTABLE MOVES THROUGH
    // A SOCKET TOO QUICKLY, THE SOCKET WILL SET THE INTERACTABLE'S
    // INTERACTION LAYER TO placeLayer BUT IT WILL NOT ACTUALLY SELECT THE
    // OBJECT - THEREFORE THE PLAYER CANNOT GRAB THE ITEM REQUIRED TO 
    // PROGRESS THE MODULE. THIS IS A FATAL BUG THAT REQUIRES A RESTART
    // OF THE MODULE.
    // Though rare, it is not worth the risk of ruining a player's experiece
    // to enable this functionality.

    /*
    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.CompareTag(targetTag);
    }
    private bool HoverGuard(IXRHoverInteractable interactable)
    {
        // ensures object is within the rotation forgiveness
        float angleDiff = Quaternion.Angle(socketRotation, interactable.transform.rotation);
        if (angleDiff > rotationForgiveness || !interactable.transform.CompareTag(targetTag)) return false;

        // ensures object is within all secondary colliders
        foreach (SecondaryCollider collider in secondaryColliders)
        {
            if (!collider.objectIsTouching) return false;
        }

        return true;
    }
    */
    public void LogMessage(string message) { print(message); }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && SelectionGuard(interactable);
    }

    private bool SelectionGuard(IXRSelectInteractable interactable)
    {
        // ensures object is within the rotation forgiveness
        float angleDiff = Quaternion.Angle(socketRotation, interactable.transform.rotation);
        
        if (angleDiff > rotationForgiveness || !interactable.transform.CompareTag(targetTag)) return false;

        // ensures object is within all secondary colliders
        foreach (SecondaryCollider collider in secondaryColliders)
        {
            if (!collider.objectIsTouching) return false;
        }
        return true;
    }
    /*


    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        hovering = false;
        hoveredObject = null;
    }
    */
    
    protected override void OnHoverEntering(HoverEnterEventArgs args)
    {
        if (args.interactableObject.transform.tag == targetTag)
        {        
            base.OnHoverEntering(args);
        }
    }
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactableObject.transform.tag == targetTag)
        {        
            base.OnHoverEntered(args);
        }
    }
    
    protected override void OnHoverExiting(HoverExitEventArgs args)
    {
        if (args.interactableObject.transform.tag == targetTag)
        {        
            base.OnHoverExiting(args);
        }
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Transform iTrans = args.interactableObject.transform;
        CustomGrabInteractable grabInteractable = iTrans.GetComponent<CustomGrabInteractable>();

        if (grabInteractable.gameObject.tag == targetTag)
        {
            grabInteractable.ToggleGrabbable(false);
            placed = true;
            grabInteractable.transform.rotation = transform.rotation;

            // enable this if you want the mesh to be replaced by a lower quality, more optimized mesh, once it is placed.
            if (replaceWithOptimized && optimizedMesh != null)
            {
                objectMesh.gameObject.SetActive(false);
                optimizedMesh.SetActive(true);
            }

            if (audioSource != null && playSoundEffect) StartCoroutine(PlayAudio());
            if (particles != null && playParticles) StartCoroutine(PlayParticles());
            Debug.Log(audioSource);
            if (socketMesh != null) socketMesh.gameObject.SetActive(false);

            if (!replaceWithOptimized && willHideObject)
            {
                if (objectMesh != null) objectMesh.gameObject.SetActive(false);
                iTrans.gameObject.GetComponent<BoxCollider>().enabled = false;
                foreach (SecondaryCollider collider in secondaryColliders)
                {
                    collider.gameObject.SetActive(false);
                }

                this.GetComponent<BoxCollider>().enabled = false;
            }

            base.OnSelectEntered(args);
        }
    }

    IEnumerator PlayAudio()
    {
        audioSource.Play();
        yield return new WaitForSeconds(2);
        audioSource.gameObject.SetActive(false);
        audioSource = null;
    }

    IEnumerator PlayParticles()
    {
        particles.Play();
        yield return new WaitForSeconds(2);
        particles.gameObject.SetActive(false);
        particles = null;
    }

    //ignore collisions once the object has enetered the colliders
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Collider otherCollider = collision.collider;
            Collider thisCollider = GetComponent<Collider>();
            Physics.IgnoreCollision(thisCollider, otherCollider);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Optionally, re-enable collision if needed
            Collider otherCollider = collision.collider;
            Collider thisCollider = GetComponent<Collider>();
            Physics.IgnoreCollision(thisCollider, otherCollider, false);

            //Ensure the gameobject is grabbable
            CustomGrabInteractable grabInteractable = gameObject.GetComponent<CustomGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.playerCanGrab = true;
            }
            else
            {
                Debug.LogWarning("CustomGrabInteractable component not found on the game object.");
            }
        }
    }

    /*
    private void Update()
    {
        if (!placed && hovering) WhileHovering();
    }

    private void WhileHovering()
    {
        if (hoveredObject != null && HoverGuard(hoveredObject))
        {
            hoveredObject.transform.GetComponent<CustomGrabInteractable>().interactionLayers = placeLayer;
        }
    }
    */
}
