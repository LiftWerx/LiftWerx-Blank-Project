using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRInputManager : MonoBehaviour
{

    // Creates a singleton
    public static XRInputManager Instance;

    private UnityEngine.XR.InputDevice rightController;
    private UnityEngine.XR.InputDevice leftController;

    // Start is called before the first frame update
    void Awake()
    {
        // creates a singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Update()
    {
        // Initializes devices in not found
        if (!rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref rightController);
        if (!leftController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref leftController);

    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputChars, ref InputDevice device)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputChars, devices);

        if (devices.Count > 0) device = devices[0];
    }

    public InputDevice GetRightController() { return rightController; }
    public InputDevice GetLeftController() {  return leftController; }
}
