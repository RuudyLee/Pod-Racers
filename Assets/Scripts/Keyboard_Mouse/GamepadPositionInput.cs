using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPositionInput : ControllerPositionInput
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        positionInput.left = Input.GetAxis("LeftJoystick");
        positionInput.right = Input.GetAxis("RightJoystick");
    }
}
