using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    Transform pickup;
    bool holdingObject = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pickup && Input.GetMouseButtonDown(0))
        {
            pickup.GetComponent<Grabbable>().Unhighlight();
            FixedJoint fj = gameObject.AddComponent<FixedJoint>();
            fj.connectedBody = pickup.GetComponent<Rigidbody>();
            holdingObject = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            pickup.GetComponent<Grabbable>().Highlight();
            Destroy(GetComponent<FixedJoint>());
            holdingObject = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!holdingObject)
        {
            Grabbable g = other.GetComponent<Grabbable>();
            if (g)
            {
                pickup = other.transform;
                pickup.GetComponent<Grabbable>().Highlight();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!holdingObject)
        {
            pickup.GetComponent<Grabbable>().Unhighlight();
            pickup = null;
        }
    }
}
