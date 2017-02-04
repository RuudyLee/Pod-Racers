using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodStabilizer : MonoBehaviour
{
    public float stability = 0.02f;

    float xRotationLimit = 20f;
    float yRotationLimit = 20f;
    float zRotationLimit = 20f;
    Rigidbody rb;
    CapsuleCollider cc;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, cc.radius + 0.5f))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Vector3 groundNormal = hit.normal;
            Vector3 probablyForward = Vector3.Cross(transform.right, groundNormal);
            Quaternion probableDirection = Quaternion.LookRotation(probablyForward, groundNormal);

            if (transform.rotation.eulerAngles.x > xRotationLimit)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, probableDirection, stability);
            }

            if (transform.rotation.eulerAngles.y > yRotationLimit)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, probableDirection, stability);
            }

            if (transform.rotation.eulerAngles.z > zRotationLimit)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, probableDirection, stability);
            }
        }
        else
        {
            // The pod is off the ground
            rb.constraints = RigidbodyConstraints.None;
        }

    }
}

