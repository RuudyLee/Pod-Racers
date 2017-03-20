using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unflipper : MonoBehaviour
{
    public SteamVR_ControllerManager controllerManager;
    SteamVR_TrackedObject leftTrackedObj, rightTrackedObj;
    SteamVR_Controller.Device leftDevice { get { return SteamVR_Controller.Input((int)leftTrackedObj.index); } }
    SteamVR_Controller.Device rightDevice { get { return SteamVR_Controller.Input((int)rightTrackedObj.index); } }

    // Use this for initialization
    void Start()
    {
        leftTrackedObj = controllerManager.left.GetComponent<SteamVR_TrackedObject>();
        rightTrackedObj = controllerManager.right.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip) || leftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            transform.Translate(0.0f, 10.0f, 0.0f);
            transform.rotation = new Quaternion(Quaternion.identity.x, transform.rotation.y, Quaternion.identity.z, transform.rotation.w);
        }
    }
}
