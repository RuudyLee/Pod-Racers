using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodEngine : MonoBehaviour
{
    public float speed = 5f;
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
