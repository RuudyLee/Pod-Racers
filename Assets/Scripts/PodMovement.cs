using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PodMovement : MonoBehaviour
{
    public float speed = 5f;

    ControllerPositionInput cpi;

    // Use this for initialization
    void Start()
    {
        cpi = GetComponent<ControllerPositionInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // all required input data is retreived from CPI
        transform.position += cpi.playerHead.transform.forward * cpi.positionInput.left * speed * Time.deltaTime;
    }
}
