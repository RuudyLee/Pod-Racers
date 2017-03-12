using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDrag : MonoBehaviour
{
    public float friction = 0.9f;
    public ControllerPositionInput cpi;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // drag
        if (cpi.bothEnginesOff)
        {
            rb.velocity *= 0.97f;
            rb.angularVelocity *= 0.97f;
            
        }
        else
        {
            rb.velocity *= friction;

            // encourage the rigidbody to move straight if player is trying to go straight
            if (Mathf.Abs(cpi.positionInput.left - cpi.positionInput.right) < 0.01f)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * rb.velocity.magnitude, 0.05f);
            }
        }
    }
}
