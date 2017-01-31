using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RodMovement : MonoBehaviour
{
    public float speed = 0.1f;

    SteamVR_ControllerManager controllerManager;
    SteamVR_Controller.Device device;

    public GameObject playerHead;
    GameObject leftController;
    SteamVR_TrackedObject trackedObj;

    Vector2 touchpad;
    public Vector2 positionInput;

    // Use this for initialization
    void Start()
    {
        controllerManager = GetComponent<SteamVR_ControllerManager>();
        leftController = controllerManager.left;
        trackedObj = leftController.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // get controller device instance
        device = SteamVR_Controller.Input((int)trackedObj.index);

        // Controller position
        float zeroPos = 0.035f;
        float maxPos = 0.05f;
        
        positionInput.y = ((leftController.transform.position.z - playerHead.transform.position.z) - zeroPos) / (maxPos - zeroPos);
        //positionInput.y = leftController.transform.position.z - playerHead.transform.position.z;
        // 0.1 = default
        // 0.12 = max

        if (device.GetHairTrigger())
        {
            transform.position -= transform.forward * positionInput.y * speed * Time.deltaTime;
        }

    
        // Trackpad
        
        // if finger is on touchpad
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

            transform.position -= transform.forward * touchpad.y * speed * Time.deltaTime;
        }
    }
}
