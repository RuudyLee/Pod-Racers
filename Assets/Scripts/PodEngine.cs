﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodEngine : MonoBehaviour
{
    public float speed = 50000f;
    public float lurkSpeed = 10000f;
    public ControllerPositionInput cpi;
    public CapsuleCollider bodyCollider;

    Rigidbody rb;
    public bool left = true;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spherecast downwards to assist climbing uneven ground
        RaycastHit hitInfoDown;
        if (Physics.SphereCast(transform.position,
                               bodyCollider.radius,
                               Vector3.down,
                               out hitInfoDown,
                               ((bodyCollider.height / 2f) - bodyCollider.radius) + 0.01f)) // add a small amount to help the spherecast
        {
            if (Mathf.Abs(Vector3.Angle(hitInfoDown.normal, Vector3.up)) < 85f)
            {
                float magnitude = rb.velocity.magnitude;
                rb.velocity = Vector3.ProjectOnPlane(rb.velocity, hitInfoDown.normal).normalized * magnitude;
            }
        }
    }

    void FixedUpdate()
    {
        // Read inputs and apply force accordingly
        float force;
        if (left)
        {
            force = cpi.positionInput.left;
        }
        else
        {
            force = cpi.positionInput.right;
        }
        rb.AddForce(transform.forward * force * speed * Time.deltaTime);

        
    }
}
