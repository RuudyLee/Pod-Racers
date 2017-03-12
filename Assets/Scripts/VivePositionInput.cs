using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VivePositionInput : ControllerPositionInput
{
    public float deadZoneWidth = 0.005f;        // width of the controller deadzone
    public float zeroPosition = 0.035f;         // position at which input will be 0
    public float maxPosition = 0.05f;           // position at which input will be 1
    public float throttleAcceleration = 0.2f;   // the speed at which the throttle moves
    public bool forceTriggerPress = true;       // enable to output input on trigger press only

    SteamVR_ControllerManager controllerManager;
    SteamVR_TrackedObject leftTrackedObj, rightTrackedObj;
    SteamVR_Controller.Device leftDevice { get { return SteamVR_Controller.Input((int)leftTrackedObj.index);  } }
    SteamVR_Controller.Device rightDevice { get { return SteamVR_Controller.Input((int)rightTrackedObj.index); } }

    // Use this for initialization
    void Start()
    {
        controllerManager = GetComponent<SteamVR_ControllerManager>();
        leftTrackedObj = controllerManager.left.GetComponent<SteamVR_TrackedObject>();
        rightTrackedObj = controllerManager.right.GetComponent<SteamVR_TrackedObject>();
        bothEnginesOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Update states
        bothEnginesOff = !(leftDevice.GetHairTrigger() || rightDevice.GetHairTrigger());
        
        if (leftDevice.GetHairTrigger())
        {
            leftDevice.TriggerHapticPulse(1000);
            positionInput.left = Mathf.Clamp(calculateInputValue(leftTrackedObj, positionInput.left), 0.0f, 1.0f);
        }
        else
        {
            // engine is off, lerp throttle back to 0
            positionInput.left = Mathf.Lerp(positionInput.left, 0.0f, throttleAcceleration);
        }
        
        if (rightDevice.GetHairTrigger())
        {
            rightDevice.TriggerHapticPulse(1000);
            positionInput.right = Mathf.Clamp(calculateInputValue(rightTrackedObj, positionInput.right), 0.0f, 1.0f);
        }
        else
        {
            positionInput.right = Mathf.Lerp(positionInput.right, 0.0f, throttleAcceleration);
        }
    }

    private float calculateInputValue(SteamVR_TrackedObject trackedObj, float input)
    {
        Vector3 eyeToController = trackedObj.transform.position - transform.position;   // vector from the eye to the controller
        float displacement = Vector3.Dot(eyeToController, transform.forward);           // length of vector going towards forward

        // get a value from 0 to 1 based on calibration settings and slowly
        // accelerate throttle to that value
        // clamped to 0 and 1 incase player goes beyond calibrated settings
        // can pod racers go backwards? change 0.0f to something like -0.2f if it can
        return Mathf.Lerp(input, (displacement - zeroPosition) / (maxPosition - zeroPosition), throttleAcceleration);
    }
}
