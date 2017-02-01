using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPositionInput : ControllerPositionInput
{
    public float smoothing = 1000f;
    public KeyCode leftThrottle = KeyCode.Q;
    public KeyCode rightThrottle = KeyCode.E;

    // Use this for initialization
    void Start()
    {
        positionInput.left = 0f;
        positionInput.right = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // left throttle key is pressed
        if (Input.GetKey(leftThrottle))
        {
            positionInput.left += smoothing * Time.deltaTime;
        }
        else
        {
            positionInput.left -= smoothing * Time.deltaTime;
        }
        positionInput.left = Mathf.Clamp(positionInput.left, 0f, 1f);

        // right throttle key is pressed
        if (Input.GetKey(rightThrottle))
        {
            positionInput.right += smoothing * Time.deltaTime;
        }
        else
        {
            positionInput.right -= smoothing * Time.deltaTime;
        }
        positionInput.right = Mathf.Clamp(positionInput.right, 0f, 1f);

        Debug.Log(positionInput.left);
    }
}
