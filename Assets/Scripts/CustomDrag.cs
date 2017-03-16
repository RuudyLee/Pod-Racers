using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDrag : MonoBehaviour
{
    public float friction = 0.9f;
    public float gravity = 100f;
    public ControllerPositionInput cpi;

    Rigidbody rb;
    CapsuleCollider cc;
    bool grounded = false;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gravity
        RaycastHit hitInfoDown;
        if (Physics.Raycast(transform.position,
                               Vector3.down,
                               out hitInfoDown,
                               cc.radius + 0.1f))
        {
            rb.velocity = Vector3.ProjectOnPlane(rb.velocity, hitInfoDown.normal);
            grounded = true;
        }
        else
        {
            grounded = false;
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }

        rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * rb.velocity.magnitude, 0.05f);
    }
}
