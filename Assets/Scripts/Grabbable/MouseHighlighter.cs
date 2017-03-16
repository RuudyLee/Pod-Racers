using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlighter : MonoBehaviour
{

    GameObject pickup;
    Vector3 screenPoint;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // we're holding an object
        if (pickup)
        {
            // move object with mouse
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            pickup.transform.position = curPosition;

            // let go of the object
            if (Input.GetMouseButtonUp(0))
            {
                pickup.GetComponent<Grabbable>().Unhighlight();
                pickup = null;
            }
        }

    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Grabbable g = hit.collider.GetComponent<Grabbable>();
            if (g)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    g.Highlight();
                    pickup = g.gameObject;
                    screenPoint = Camera.main.WorldToScreenPoint(pickup.transform.position);
                    offset = pickup.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                }
            }
        }
    }
}
