using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodEngine : MonoBehaviour
{
    public float speed = 5f;
    public float lurkSpeed = 0.2f;

    ControllerPositionInput cpi;
    Rigidbody rb;
    public bool left = true;

    // Use this for initialization
    void Start()
    {
        cpi = transform.parent.GetComponent<ControllerPositionInput>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Add a constant force forwards, feels more natural this way
        if (cpi.positionInput.left > 0 || cpi.positionInput.right > 0)
        {
            rb.AddForce(transform.up * speed * Time.deltaTime);
        }

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
        rb.AddForce(transform.up * force * speed * Time.deltaTime);
    }
}
