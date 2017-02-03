using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hover : MonoBehaviour {

    public Rigidbody PodBase;

    int layermask;
    public float hoverHeight = 4.0f;
    public float hoverForce = 60.0f;
    public GameObject[] hoverPoints;

    public float speed = 5.0f;

    public Transform L_thrusterPoint;
    public Transform R_thrusterPoint;

    float thrustValL;
    float thrustValR;


	// Use this for initialization
	void Start () {

        layermask = 1 << LayerMask.NameToLayer("RacePod");
        layermask = ~layermask;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            thrustValL = Input.GetAxis("Vertical");
        }
        else
        {
            thrustValL = 0.0f;
        }
        

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            thrustValR = Input.GetAxis("Vertical");
        }
        else
        {
            thrustValR = 0.0f;
        }
	}

    void FixedUpdate()
    {
        /////////hover logic is right here//////////
        RaycastHit hit;
        for(int i = 0; i < hoverPoints.Length; i ++)
        {
            var currPoint = hoverPoints[i];

            if(Physics.Raycast(currPoint.transform.position,
                -Vector3.up,
                out hit,
                hoverHeight,
                layermask))
            {
                PodBase.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), currPoint.transform.position);
            }
            else
            {
                if(transform.position.y > currPoint.transform.position.y)
                {
                    PodBase.AddForceAtPosition(currPoint.transform.up * hoverForce, currPoint.transform.position);
                }
                else
                {
                    PodBase.AddForceAtPosition(currPoint.transform.up * -hoverForce, currPoint.transform.position);
                }
            }
        }


        ////////experimental turning logic//////////
        PodBase.AddForceAtPosition(L_thrusterPoint.transform.forward * speed * 25 * thrustValL, L_thrusterPoint.position);
        PodBase.AddForceAtPosition(R_thrusterPoint.transform.forward * speed * 25 * thrustValR, R_thrusterPoint.position);
    }
}
