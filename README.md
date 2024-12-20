# LiftWerx Blank Project

## Table of Contents
- [Welcome](#Welcome!!)
- [Getting Started](#getting-started)
- [Creating a Training Module](#Creating-a-Training-Module)
- [Scripting References](#Scripting-References)

## Welcome!!
Welcome to the LiftWerx team! This blank project has been created as a starting point to assist you in your VR development journey. This ReadMe file contains important information on how to create a cohesive training module, as well as documentation on the scripts you will be using.

First off, welcome! We hope you will enjoy developing as much as previous team members did during their terms. For any additional information that may be missing from this GitHub documentation, please refer to the LiftWerx Sharepoint location provided by one of our previous developers, Scott:
<br> ```Documents/Projects/00 - North America/Internal Projects/NA-IN-00271 - Interface Installation Training Tools/04 - Documents/Technical Documentation```

For technical questions related to the training modules, including details about the installation process for cranes, rigging, and other components, it is recommended to talk to Peter or other engineers.

If you identify opportunities to improve this project or any of the other projects, please do so. The goal is for this project to continually advance, and as one of the developers, you are now an integral part of its ongoing development. No pressure!

## Getting Started
**Prerequisites**
- Unity 2022.3.29f1 https://unity.com/download
- Android Studio Koala https://developer.android.com/studio
- AnchorPoint https://www.anchorpoint.app/
- Blender and SolidWorks
- Visit the Sharepoint folder and open the accounts info for vr@liftwerx.com to sign in to GitHub, AnchorPoint, and AWS (Only sign into AWS if you are editing the Project LiftWerx Academy). 

1. **Install AnchorPoint**
- Download AnchorPoint ``` https://www.anchorpoint.app/ ```
- Login with your LiftWerx Email
- Follow this tutorial (if you're creating a new project) to sync your project with the GitHub project. ```https://youtu.be/-8RV6OiYTFw```
- I would probably clone the repository then follow the above tutorial to connect the downloaded version with a new repository so that you can keep this project for future reference
- Use this as the repository key for GitHub ```[https://github.com/LiftWerx/LiftWerx-Academy.git](https://github.com/LiftWerx/LiftWerx-Blank-Project.git)``` or whatever your repository key is

2. **Launch Unity Hub**
- Click on "Add"
- Select the folder that AnchorPoint downloaded your project repository to

3. **Install Android Studio**
- Install Android Studio ```https://developer.android.com/studio```
- Configure LogCat to log error messages (this step is only for detailed logging, most of the time you'll catch the errors in Unity)

4 **Download 3D Modelling Software**
- Ask Peter for the hardrive with the LiftWerx Solidworks account, if he hasn't given it already
- Download Blender `https://www.blender.org/`

5. **You're Set!**
- Make sure to be careful with pushing commits
- You're setup with version control in Unity!

## Creating a Training Module
### Initial Planning and BrainStorming
Once the project is set up in Unity and all necessary software is downloaded, the next step is to start planning your training module. Begin by creating a detailed script that includes voicelines, interactions, and events. Additionally, develop an object codex table, which includes a 'codex' or popup screen with information about interactable items. Examples of both the script and the codex table can be found on LiftWerx SharePoint here:
`Documents/Projects/00 - North America/Internal Projects/NA-IN-00271 - Interface Installation Training Tools/04 - Documents/Misc`

### Using the Director to Influence the Timeline
Once the script is approved, you can start working on the Unity project. The Director is a tool that allows for quick planning and modification of the training module's timing, which is essential given the numerous 'signals' (key events) involved. Detailed documentation on using the Director and recording voicelines is available from Scott in the technical documents on SharePoint:
`Documents/Projects/00 - North America/Internal Projects/NA-IN-00271 - Interface Installation Training Tools/04 - Documents/Technical Documentation`

### General Pointers When Creating a Training Module
**Creating an Interactable Item and Using 'Create Custom Socket'**
- A plugin is available to simplify creating grab interactables. Access it via `window/Create Custom Socket`
- Assign the 'hologram material' to the 'Custom Material' property and assign the 'onHover' material to the Hover Material property. You will find these materials in `Assets/Project/Materials and Textures`
- Assign the Object Codex Prefab to the Object Codex property which is located in Assets
- Select the mesh that you would like to make interactable, then click 'Duplicate'

**If The Project Lags In The Oculus**
- Use `window/Count Triangles` to identify any meshes that may be too large. This function counts triangles on all meshes in the project.
- Use the Oculus Profiler and enable persistent overlay (This can be found in your apps on the Quest 3)
- Check for script errors that might be throwing errors and causing lag.

**Using the BroadCast Manager**
- The Broadcast Manager allows you to send messages to LiftWerx Academy. This pluign should already be in your project in `Plugins/Android/libs`. This location should also include the Kotlin Libraries
- You can call this plugin using the Broadcast Manager Prefab located in the Assets folder. It is also in the scene.
- Currently it has two signals. Start Module and End Module. The Message is the important part. They must emit 'Started' at the start and 'Completed' at the end. This is so that LiftWerx Academy can correctly interpret the signals.
- You can call BroadcastManager.EmitSignal(int) to emit either signal. This should be done in the Director at the start and end of the module
- If you have a checkpoint you would like to create, you can add a signal to the list. You must also add this to the status table for LiftWerx Academy Database to make it a valid status. Make sure that the message and the name in 'Status' match exactly or it won't work.

**Teleportation Anchors**
- There are two sample teleportation anchors in the scene. The first is 'Sample Teleportation Anchor' which is a normal teleportation anchor that lets the user jump to it. This can be copy and pasted around the terrain to allow players to move around
- The second is the 'endSceneTeleport' this teleport kills the app. This should only turn on at the end of the module

**Building and Testing a Module**
If all Headsets have LiftWerx Academy installed on them, you need to remove it so that you can run your own project on it. I would recommend having at least one headset without LiftWerx Academy for development purposes. To remove LiftWerx Academy, a factory reset is required. You can follow Meta's guide but its pretty straight forward. If you want to keep LiftWerx Academy on it, you can use sidequest or the command line to install LiftWerx Academy onto the headset. This will kill the current version and as long as you don't open LiftWerx Academy, it won't force itself open.

## Scripting References
This section includes a combined list of documentation from all contributors. If any information is unclear, it is advisable to refer to completed projects such as Wind Turbine Fundamentals or TeleHook Installation to see how similar tasks are handled.

### **Blinker.cs** (used on the arrow blinker prefab)

**Properties**
- `int blinks`: the number of times the object will appear and disappear
- `float timeBetweenBlinks`: interval in seconds between appearances
- `float initialDelay`: how long from being called to the first blink
- `bool destroyOnEnd`: whether the GameObject will be deleted when finished blinking
- `float hideDelay`: how long object will remain visible on the last blink

**Public Methods**
- `void Blink()`: begins object blinking

---

### **CodexControls.cs** (manages an individual object’s codex)

*Note*: To identify the codex, the codex must be a sibling of this component’s GameObject and have the name specified in `GameManager.codexObjectName`.

**Properties**
- `bool canOpenCodex`: whether this component can open the codex it controls

**Public Methods**
- `void SetCanOpenCodex(bool canOpen)`: sets the `CanOpenCodex` property with true/false
- `void EnableCodex()`: hides all other codexes and enables this one

---

### **CustomGrabInteractable.cs** (put on objects that should be grabbed/clicked on by player)

**Properties**
- `bool playerCanGrab`: whether the player can grab and move this object
- `CustomGrabTransformer transformer`: the transformer associated with this object which allows the player to rotate it
- `bool respectBoundaries`: whether the object will float back to a boundary’s rest point if touching the boundary
- `InteractionLayerMask interactionLayersForReset`: the base XR interaction layers an object with this component attached should reside on

**Public Methods**
- `void ToggleGrabbable(bool grabbable)`: sets the `playerCanGrab` property
- `void SetRespectBoundaries(bool respects)`: sets the `respectBoundaries` property
- `void ResetInteractionLayers()`: sets the object's `interactionLayers` to `interactionLayersForReset`

---

### **CustomGrabTransformer.cs** (put on objects with the CustomGrabInteractable script – enables rotation)

**Properties**
- `bool playerCanRotate`: whether the player can rotate an object via thumbstick while grabbing it
- `float rotationSpeed`: the speed at which the object will rotate when the thumbstick is pressed

**Public Methods**
- `override void Process(...)`: handles the rotation transformation applied to the GameObject
- `void SetCurrentInteractor(XRBaseInteractor interactor)`: updates the `currentInteractor` to whatever is interacting with this GameObject

---

### **CustomSocketInteractor.cs**

**Properties**
- `string targetTag`: the tag an interactable must have for the socket to select it
- `float rotationForgiveness`: the max difference in angle the socket must have with respect to an interactable object to select it
- `InteractionLayerMask placeLayer`: (deprecated)
- `bool playsoundEffect`: whether a sound effect will play when the socket selects an interactable
- `bool playParticles`: whether a particle effect will play when the socket selects an interactable
- `bool willHideObject`: whether the socket will make the object disappear when it is selected

---

### **CustomTeleportAnchor.cs**

**Properties**
- `bool willReappear`: whether the anchor will reappear after you teleport away from it
- `Transform linkedPosition`: a transform of where the player will end up (and face) when teleporting to this anchor

**Public Methods**
- `void SetLinkedPosition`: setter
- `void SetWillReappear`: setter

---

### **GameManager.cs** (manages codex and teleport anchor enabling/disabling)

**Properties**
- `static GameManager Instance`: an instance of the GameManager accessible from any other script
- `float hideAnchorDelay`: delay between initiating a teleportation to an anchor and hiding its visuals

**Public Methods**
- `void HideAllCodexes()`: sets all codexes inactive

---

### **XRInputManager.cs** (interfaces with the XR controllers so we can manually read input to perform actions)

**Properties**
- `static XRInputManager Instance`: an instance of the XRInputManager accessible from any other script

**Public Methods**
- `InputDevice GetRightController()`: returns the right controller to access its input properties
- `InputDevice GetLeftController()`: returns the left controller to access its input properties

---

### **InputActionActivation.cs** (manages ray interactor behavior and other button presses)

**Properties**
- `XRRayInteractor rightRayInteractor`: the ray interactor for clicking/grabbing (right hand)
- `XRRayInteractor leftRayInteractor`: the ray interactor for clicking/grabbing (left hand)
- `ActionBasedSnapTurnProvider turnProvider`: the component that manages snap turning
- `Transform leftTeleportInteractor`: the ray interactor for teleporting (left hand)
- `Transform rightTeleportInteractor`: the ray interactor for teleporting (right hand)

---

### **Narrator.cs** (manages playback of voicelines on button press)

**Properties**
- `PlayableDirector director`: the director with whose timeline this narrator is associated with

**Public Methods**
- `void SetCheckpoint()`: Stores the current time on the Director's timeline
- `void PlayAtCheckPoint()`: Plays the director's timeline at the most recently stored time

---

### **ObjectBoundary.cs** (when an interactable touches a GameObject with this script, it will be freed from the player's control and reset to a set position)

**Properties**
- `Transform resetTransform`: where the object will reset to
- `InteractionLayerMask resetLayer`: the layer the object will exist on while resetting (player should not be able to interact with this layer)
- `InteractionLayerMask grabAndPlaceLayer`: the layer to set the object to once reset
- `float resetDuration`: how long it takes for objects to return to the `resetTransform`'s position

---

### **OnTeleport.cs**

**Properties**
- `UnityEvent Event`: actions to take when the player teleports to this anchor

---

### **Outline.cs**

**IMPORTANT NOTE**: Outlines have different modes; however, the only ones used so far in our modules are `OutlineAll` and `OutlineHidden`.

- `OutlineAll` will draw an outline around an object whether it is in your direct line of sight or if it is behind another object.
- `OutlineHidden` will only display the outline if behind an object.

**Public Methods**
- `void DisableOutline()`: sets outline mode to `OutlineHidden` (NOT TO BE CONFUSED WITH DISABLING THE ENTIRE SCRIPT, WHICH REMOVES THE OUTLINE ALTOGETHER)
- `void ReenableOutline()`: sets outline mode to `OutlineAll`

---

### **PlaySteps.cs** (see Document 4: Creating a Cohesive Training Module using the Director)

**Properties**
- `static PlaySteps Instance`: an instance of the PlaySteps script accessible from any other script
- `bool sequenced`: When enabled, a step will not play if the step before it has not played
- `PlayableDirector director`: a reference to the director whose timeline `PlaySteps` will control
- `List<Step> steps`: a list of Steps that can be played in sequence. The order of these steps matters when calling `PlayNextStep()`

**Class Step:**
- `string name`: a simple identifier that helps developers understand what should occur at that step
- `float time`: how many seconds into playback the timeline will resume playing at
- `bool hasPlayed`: whether the step has played yet

**Public Methods**
- `void PlayStepIndex(int index)`: plays the step at the specified index in the list (starting from 0)
- `void PlayNextStep()`: plays the step after the step previously played. Starts at 0

---

### **Stepper.cs** (allows GameObjects to communicate with the `PlaySteps` instance present in the scene)

**Properties**
- `bool canStep`: whether this script can currently call `PlaySteps.Instance.PlayNextStep()`

**Public Methods**
- `void TryNextStep()`: will attempt to call `PlaySteps.Instance.PlayNextStep()`
- `void AllowStep()`: sets `canStep` to true

---

### **ProgrammaticTransformAnimator.cs** (used to animate movement of objects via specified displacement and rotations over time)

**NOTE**: Uses global position and rotation. If two programmatic transform animators are playing on a parent GameObject and a child GameObject, the child will only adhere to the closest programmatic transform animator in the hierarchy (in this case, the one that is attached to it).

**Properties**
- `bool canDisplace`: whether this script can set the GameObject’s position
- `bool canRotate`: whether this script can set the GameObject’s rotation
- `bool reenableInteractable`: this script disables custom grab interactables upon playing; set this true to reenable it after the animation terminates
- `bool destroyOnFinish`: whether to destroy the GameObject when the animation terminates
- `List<AnimationStep> steps`: a list of animations that will play in sequence

**Class AnimationStep:**
- `Vector3 displace`: how far the object will move along the X, Y, Z axes
- `Vector3 targetRotation`: how much the object will rotate around the X, Y, Z axes
- `float duration`: how long it takes the animation to play
- `float pauseDurationAfterStep`: how long to wait after a step before playing the next one
- `UnityEvent eventPlayedBeforePause`: actions to perform between the ending of an animation step and the beginning of the pause that follows it

**Public Methods**
- `void Play()`: plays all the animation steps in sequence
- `void SetCanDisplace(bool can)`: setter
- `void SetCanRotate(bool can)`: setter

---

### **SecondaryCollider.cs** (acts as additional colliders for `CustomSocketInteractors` to ensure that the interactable is fitting the object well)

**Properties**
- `bool objectIsTouching`: is an object touching this collider
- `InteractionLayerMask grabAndPlaceLayer`: the layer to set the object if leaving this collider

---

### **SelectAllPiecesTutorial.cs** (a script that requires a certain number of actions to be performed by the player before an event occurs – used in the tutorial module)

**Properties**
- `int totalPieces`: how many unique actions the player needs to complete before the event occurs
- `UnityEvent Complete`: the events that will occur once all actions are performed

**Public Methods**
- `void SelectPiece(string name)`: call this to add `name` to the list of completed actions

---

### **CreateSocket.cs** (This should help create a socket and interactable more quickly. Go to window/create custom socket, then select the mesh you would like to create an interactor and socket for)

**Properties**
- `string codexObjectName`: Name of the codex object (default: "Object Codex")
- `Material customMaterial`: Custom material to be applied to duplicated object
- `Material HoverMaterial`: Material to be used when hovering over the socket
- `GameObject ObjectCodex`: Prefab of the object codex

**Public Methods**
- `static void ShowWindow()`: Displays the Create Socket editor window
- `void OnGUI()`: Draws the custom editor GUI elements
- `void DuplicateObjectWithMaterialAndScripts()`: Duplicates the selected object, creates and configures a custom socket with materials, scripts, and colliders
- `GameObject resetTransform(GameObject g)`: Resets the transform of a GameObject to its default position, rotation, and scale

**Private Methods**
- `void DuplicateObjectWithMaterialAndScripts()`: Creates and configures the custom socket by:
  - Instantiating a new GameObject and adding required components
  - Creating child GameObjects with colliders
  - Duplicating the selected object and applying custom materials
  - Setting up parent-child relationships and adding additional scripts
  - Configuring properties of `CustomSocketInteractor`, `CustomGrabInteractable`, and `Outline`
  - Resetting the transform of involved GameObjects

---

### **TriangleCounter.cs** (Editor script for counting triangles in the scene. Its usefull for optimization)

**Public Methods**
- `static void ShowWindow()`: Displays the TriangleCounter editor window
- `void OnGUI()`: Draws the custom editor GUI elements
- `void CountTriangles()`: Counts and logs the number of triangles for each root GameObject in the active scene

**Details**
- **MenuItem**: `"Window/Count Triangles in Scene"` - Adds a menu item to open the TriangleCounter window
- **OnGUI**: Creates a button in the editor window to trigger the `CountTriangles` method
- **CountTriangles**: Iterates through all root GameObjects in the scene and their MeshFilters to count the number of triangles in their meshes

**Usage**
1. Open the TriangleCounter window from the Unity Editor menu: `Window` > `Count Triangles in Scene`.
2. Click the "Count" button to execute the triangle counting process.
3. The number of triangles for each root GameObject will be logged to the console.

---

### Broadcast Manager.cs (Sends a broadcast message to Liftwerx Academy)
**Properties**
- `public List<BroadcastUpdate> broadcastUpdate`: A list of broadcast events, each containing a name and a message to be broadcast.

**Classes**
- **BroadcastUpdate**: A serializable class that represents a broadcast event.
  - `public string name`: The name of the broadcast event.
  - `public string Message`: The message to be broadcast.

**Public Methods**
- `public void EmitSignal(int MessageNumber)`: Emits a broadcast signal based on the specified `MessageNumber`. Retrieves the corresponding `BroadcastUpdate` from the `broadcastUpdate` list and calls `SendBroadcast` with its message.
- `public void SendBroadcast(string message)`: Sends a broadcast with the specified message. Uses Android Java classes to interact with the Android broadcast system.

**Details**
- **EmitSignal**: 
  - Retrieves the broadcast update using the provided `MessageNumber`.
  - Calls `SendBroadcast` with the message from the `BroadcastUpdate`.
  - Logs the message to the Unity console.
- **SendBroadcast**:
  - Uses `AndroidJavaClass` and `AndroidJavaObject` to interact with the Android broadcast system.
  - Catches and logs any exceptions that occur during the broadcasting process.

**Usage**
1. Add the `BroadcastManager` prefab to your scene.
2. Populate the `broadcastUpdate` list in the Unity Editor with broadcast events, each with a name and message.
3. Call `EmitSignal(int MessageNumber)` with the director at the start and end of the module and any other time you need to emit a signal.
4. Be sure you have the Broadcast Manager Plugin in Plugins/Android/libs as well as the Kotlin Std Plugins in the same folder.
