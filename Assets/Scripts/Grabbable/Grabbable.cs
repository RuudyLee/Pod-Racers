using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    bool highlighted = false;
    Renderer r;

    // Use this for initialization
    void Start()
    {
        r = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (highlighted)
        {
            r.material.color = Color.green;
        }
        else
        {
            r.material.color = Color.white;
        }
       
    }

    public void Highlight()
    {
        highlighted = true;
    }

    public void Unhighlight()
    {
        highlighted = false;
    }  
}
