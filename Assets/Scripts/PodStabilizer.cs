using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodStabilizer : MonoBehaviour
{
    public float stability = 0.02f;

    float xRotationLimit = 20f;
    float yRotationLimit = 20f;
    float zRotationLimit = 20f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2.0f))
        {
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


    }
}

