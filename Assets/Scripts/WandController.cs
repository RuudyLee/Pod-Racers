using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index);  } }
    private SteamVR_TrackedObject trackedObj;

    private GameObject pickup;
    private Transform pickupOriginalParent;
    bool holdingSomething = false;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controller.TriggerHapticPulse(10000);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && pickup != null)
        {
            pickupOriginalParent = pickup.transform.parent;
            pickup.transform.parent = this.transform;
            holdingSomething = true;
        }

        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip) && holdingSomething)
        {
            pickup.transform.parent = pickupOriginalParent;
            holdingSomething = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        controller.TriggerHapticPulse(100);
    }

    private void OnTriggerEnter(Collider other)
    {
        pickup = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!holdingSomething)
        {
            pickup = null;
        }
    }
}
