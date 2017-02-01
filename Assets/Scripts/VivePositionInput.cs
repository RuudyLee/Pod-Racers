﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float displacement;

        // left controller
        displacement = leftController.transform.position.z - playerHead.transform.position.z;

        positionInput.left = 0.0f;
        if ((!forceTriggerPress || leftDevice.GetHairTrigger()) &&
            displacement <= zeroPosition - deadZoneWidth &&
            displacement >= zeroPosition + deadZoneWidth)
        {
            // the controller is outside of the deadzone, so get the input

            // get a value from 0 to 1 based on calibration settings
            // clamped to 0 and 1 incase player goes beyond calibrated settings
            // can pod racers go backwards? change 0.0f to something like -0.2f if it can
            positionInput.left = Mathf.Clamp((displacement - zeroPosition) / (maxPosition - zeroPosition), 0.0f, 1.0f);
        }

        // now do the same thing for the right controller
        displacement = rightController.transform.position.z - playerHead.transform.position.z;

        positionInput.right = 0.0f;
        if ((!forceTriggerPress || rightDevice.GetHairTrigger()) &&
            displacement <= zeroPosition - deadZoneWidth &&
            displacement >= zeroPosition + deadZoneWidth)
        {
            positionInput.right = Mathf.Clamp((displacement - zeroPosition) / (maxPosition - zeroPosition), 0.0f, 1.0f);
        }
    }
}