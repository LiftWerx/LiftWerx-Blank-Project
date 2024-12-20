using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;
using log4net.Util;

public class CreateSocket : EditorWindow
{
    [MenuItem("Window/Create Custom Socket")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<CreateSocket>("Create Custom Socket");
    }
    public string codexObjectName = "Object Codex";

    public Material customMaterial;
    public Material HoverMaterial;
    public GameObject ObjectCodex;
    public AudioClip snapClip;
    
    bool disableOnStart = false;
        private void OnGUI()
    {
        GUILayout.Label("Create Socket", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("Assign a custom material in the Unity Editor.", MessageType.Info);

        customMaterial = EditorGUILayout.ObjectField("Custom Material", customMaterial, typeof(Material), false) as Material;
        HoverMaterial = EditorGUILayout.ObjectField("Hover Material", HoverMaterial, typeof(Material), false) as Material;
        ObjectCodex = EditorGUILayout.ObjectField("Object Codex", ObjectCodex, typeof(GameObject), false) as GameObject;
        snapClip = EditorGUILayout.ObjectField("Snap Clip", snapClip, typeof(AudioClip), false) as AudioClip;
        
        disableOnStart = EditorGUILayout.Toggle("Disable on Start", disableOnStart);

        if (GUILayout.Button("Convert to Interactable"))
        {
            DuplicateObjectWithMaterialAndScripts();
        }
    }

    private void DuplicateObjectWithMaterialAndScripts()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            // Create an empty GameObject and assign the scripts to it
            GameObject socketObject = new GameObject("CustomSocket");
            CustomSocketInteractor customSocket = socketObject.AddComponent<CustomSocketInteractor>();

            if (disableOnStart){ DisableOnStart disableOnStart = socketObject.AddComponent<DisableOnStart>();}

            Stepper stepper = socketObject.AddComponent<Stepper>();
            BoxCollider boxCollider0 = socketObject.AddComponent<BoxCollider>();
            boxCollider0.isTrigger = true;

            // Create two child GameObjects with BoxCollider and SecondaryCollider scripts
            GameObject secondaryCollider1 = new GameObject("SecondaryCollider1");
            BoxCollider boxCollider1 = secondaryCollider1.AddComponent<BoxCollider>();
            SecondaryCollider secondaryColliderScript1 = secondaryCollider1.AddComponent<SecondaryCollider>();
            secondaryCollider1.transform.parent = socketObject.transform;
            boxCollider1.isTrigger = true;

            GameObject secondaryCollider2 = new GameObject("SecondaryCollider2");
            BoxCollider boxCollider2 = secondaryCollider2.AddComponent<BoxCollider>();
            SecondaryCollider secondaryColliderScript2 = secondaryCollider2.AddComponent<SecondaryCollider>();
            secondaryCollider2.transform.parent = socketObject.transform;
            boxCollider2.isTrigger = true;

            // Create a child GameObject by duplicating the selected object
            GameObject duplicatedObject = Instantiate(selectedObject);
            duplicatedObject.name = selectedObject.name + "_Copy";
            duplicatedObject.transform.parent = socketObject.transform;
            Selection.activeGameObject = duplicatedObject;
            duplicatedObject.transform.rotation = Quaternion.identity;

            // Add custom material to all renderers in the duplicated object
            Renderer[] renderers = duplicatedObject.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer renderer in renderers)
            {
                Material[] materials = new Material[renderer.sharedMaterials.Length];
                for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                {
                    materials[i] = customMaterial;
                }
                renderer.sharedMaterials = materials;
            }

            //Create empty gameobject to hold selected object
            GameObject ParentObject = new GameObject(selectedObject.name);
            ParentObject.transform.position = selectedObject.transform.position;
            ParentObject.transform.rotation = selectedObject.transform.rotation;
            ParentObject.transform.localScale = selectedObject.transform.localScale;

            //Create empty parent 
            GameObject MiddleParent = new GameObject("Mesh");
            MiddleParent.transform.SetParent(ParentObject.transform);

            //Create Codex Object and set it as the parent of the selected object and the codex
            GameObject Codex = Instantiate(ObjectCodex);
            Codex.transform.SetParent(ParentObject.transform);
            Codex.transform.position = selectedObject.transform.position;
            selectedObject.transform.SetParent(MiddleParent.transform);
            Codex.name = "Object Codex";

            //Add scripts to the selected GameObject
            CustomGrabInteractable customGrabInteractable0 = MiddleParent.AddComponent<CustomGrabInteractable>();
            CustomGrabTransformer customGrabTransformer0 = MiddleParent.AddComponent<CustomGrabTransformer>();
            CodexControls CodexController0 = MiddleParent.AddComponent<CodexControls>();
            Outline outline0 = MiddleParent.AddComponent<Outline>();
            BoxCollider boxCollider3 = MiddleParent.AddComponent<BoxCollider>();
            boxCollider3.transform.position = MiddleParent.transform.position;
            Rigidbody rb;
            if (MiddleParent.GetComponent<Rigidbody>() == null)
            {
                rb = MiddleParent.AddComponent<Rigidbody>();
            }
            rb = MiddleParent.GetComponent<Rigidbody>(); // This line is needed as if the original object already has a rigidbody, The AddComponent above will return null.
            rb.isKinematic = true;
            rb.useGravity = false;

            //add a tag
            TagHelper.AddTag(selectedObject.name);
            MiddleParent.tag = selectedObject.name;


            Debug.Log("Created custom socket with custom material, scripts, and colliders added.");

            //Modify Transform
            socketObject.transform.position = selectedObject.transform.position;
            socketObject.transform.rotation = selectedObject.transform.rotation;
            socketObject.name = selectedObject.name + " Holo";
            duplicatedObject.transform.localPosition = Vector3.zero;
            boxCollider0.transform.position = socketObject.transform.position;

            // Socket audio source
            GameObject audioSource = new GameObject("Audio Source");
            audioSource.transform.parent = socketObject.transform;
            audioSource.transform.localPosition = Vector3.zero;
            AudioSource adsrc = audioSource.AddComponent<AudioSource>();
            adsrc.playOnAwake = false;
            if (snapClip)
            {
                adsrc.clip = snapClip;
            }

            //Assign variables
            //Custom Socket Interactor
            customSocket.socketMesh = duplicatedObject;
            customSocket.objectMesh = MiddleParent;
            customSocket.willHideObject = false;
            customSocket.targetTag = selectedObject.name;
            customSocket.interactableHoverMeshMaterial = HoverMaterial;
            customSocket.hoverSocketSnapping = true;
            customSocket.socketSnappingRadius = 1;
            customSocket.interactionLayers = InteractionLayerMask.GetMask("Grab");
            customSocket.placeLayer = InteractionLayerMask.GetMask("Place");

            secondaryColliderScript1.grabAndPlaceLayers = InteractionLayerMask.GetMask("Grab", "Place");
            secondaryColliderScript2.grabAndPlaceLayers = InteractionLayerMask.GetMask("Grab", "Place");

            //Custom Grab Interactable
            customGrabInteractable0.transformer = customGrabTransformer0;
            customGrabInteractable0.throwOnDetach = false;
            customGrabInteractable0.retainTransformParent = false;
            customGrabInteractable0.useDynamicAttach = true;
            customGrabInteractable0.matchAttachRotation = false;
            customGrabInteractable0.snapToColliderVolume = false;
            customGrabInteractable0.reinitializeDynamicAttachEverySingleGrab = false;
            customGrabInteractable0.trackRotation = false;
            customGrabInteractable0.interactionLayers = InteractionLayerMask.GetMask("Grab");
            customGrabInteractable0.respectBoundaries = true;
            if (!disableOnStart) { customGrabInteractable0.playerCanGrab = true; }
            
            //Outline
            outline0.OutlineWidth = 2f;
            outline0.OutlineColor = new Color(0, 0.4196f, 1f); // Using RGB values in the range [0, 1]


            resetTransform(selectedObject);
            resetTransform(MiddleParent);

            
        }
        else
        {
            Debug.LogWarning("No GameObject selected to duplicate.");
        }
    }
    GameObject resetTransform(GameObject g)
    {
        g.transform.localPosition = Vector3.zero;
        g.transform.localRotation = Quaternion.identity;
        g.transform.localScale = Vector3.one;
        return g;
    }
}
