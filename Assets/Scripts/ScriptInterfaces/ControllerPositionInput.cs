using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPositionInput : MonoBehaviour
{
    public GameObject playerHead;           // GameObject of the camera (in SteamVR, this is generally the (eye))
    public struct PositionInput
    {
        public float left;
        public float right;
    }
    [HideInInspector]
    public PositionInput positionInput;     // Position data to be pulled by other classes

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
