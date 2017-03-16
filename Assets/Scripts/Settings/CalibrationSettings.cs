using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum CalibrationState { CalibrateMin, CalibrateMax, StartGame };

public class CalibrationSettings : MonoBehaviour
{
    public float zeroPosition;
    public float maxPosition;

    public Text text;
    SteamVR_ControllerManager controllerManager;
    SteamVR_TrackedObject leftTrackedObj, rightTrackedObj;
    SteamVR_Controller.Device leftDevice { get { return SteamVR_Controller.Input((int)leftTrackedObj.index); } }
    SteamVR_Controller.Device rightDevice { get { return SteamVR_Controller.Input((int)rightTrackedObj.index); } }

    float timer = 6;

    CalibrationState calibrationState = CalibrationState.CalibrateMin;

    // Use this for initialization
    void Start()
    {
        controllerManager = GetComponent<SteamVR_ControllerManager>();
        leftTrackedObj = controllerManager.left.GetComponent<SteamVR_TrackedObject>();
        rightTrackedObj = controllerManager.right.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftDevice.GetHairTriggerDown() || rightDevice.GetHairTriggerDown())
        {
            if (calibrationState == CalibrationState.CalibrateMin)
            {
                // Get the average between the two hands
                zeroPosition = (getDisplacement(leftTrackedObj) + getDisplacement(rightTrackedObj)) / 2;

                text.text = "Calibration:\nHold your hands at the max position, and press back trigger on either controller";
                calibrationState = CalibrationState.CalibrateMax;
            }
            else if (calibrationState == CalibrationState.CalibrateMax)
            {
                // Get the average between the two hands
                maxPosition = (getDisplacement(leftTrackedObj) + getDisplacement(rightTrackedObj)) / 2;

                calibrationState = CalibrationState.StartGame;
            }
            else if (calibrationState == CalibrationState.StartGame)
            {
                if (timer < 0)
                {
                    SceneManager.LoadScene("test");
                }

                text.text = "Calibration Done!\n Starting in " + ((int)timer).ToString() + "...";
                timer -= Time.deltaTime;
            }
            else
            {
                Debug.Log("This should never happen");
            }
        }
    }

    private float getDisplacement(SteamVR_TrackedObject trackedObj)
    {
        Vector3 eyeToController = trackedObj.transform.position - transform.position;   // vector from the eye to the controller
        float displacement = Vector3.Dot(eyeToController, transform.forward);

        return displacement;
    }
}
