using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VivePositionInput : ControllerPositionInput
{
    public float deadZoneWidth = 0.005f;    // width of the controller deadzone
    public float zeroPosition = 0.035f;     // position at which input will be 0
    public float maxPosition = 0.05f;       // position at which input will be 1
    public bool forceTriggerPress = true;   // enable to output input on trigger press only

    SteamVR_ControllerManager controllerManager;
    GameObject leftController, rightController;
    SteamVR_TrackedObject leftTrackedObj, rightTrackedObj;
    SteamVR_Controller.Device leftDevice, rightDevice;

    // Use this for initialization
    void Start()
    {
        controllerManager = GetComponent<SteamVR_ControllerManager>();
        leftController = controllerManager.left;
        rightController = controllerManager.right;
        leftTrackedObj = leftController.GetComponent<SteamVR_TrackedObject>();
        rightTrackedObj = rightController.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // get devices
        leftDevice = SteamVR_Controller.Input((int)leftTrackedObj.index);
        rightDevice = SteamVR_Controller.Input((int)rightTrackedObj.index);

        
        // get position values based on calibration settings
        Vector3 eyeToController;
        float displacement;

        // left controller
        eyeToController = leftController.transform.position - playerHead.transform.position;
        displacement = Vector3.Dot(eyeToController, playerHead.transform.forward); // length of vector going towards forward

        positionInput.left = 0.0f;
        if (leftDevice.GetHairTrigger())
        {
            // the controller is outside of the deadzone, so get the input
            
            // get a value from 0 to 1 based on calibration settings
            // clamped to 0 and 1 incase player goes beyond calibrated settings
            // can pod racers go backwards? change 0.0f to something like -0.2f if it can
            positionInput.left = Mathf.Clamp((displacement - zeroPosition) / (maxPosition - zeroPosition), 0.0f, 1.0f);
        }

        // now do the same thing for the right controller
        eyeToController = rightController.transform.position - playerHead.transform.position;
        displacement = Vector3.Dot(eyeToController, playerHead.transform.forward);
        
        positionInput.right = 0.0f;
        if (rightDevice.GetHairTrigger())
        {
            positionInput.right = Mathf.Clamp((displacement - zeroPosition) / (maxPosition - zeroPosition), 0.0f, 1.0f);
        }
    }
}
