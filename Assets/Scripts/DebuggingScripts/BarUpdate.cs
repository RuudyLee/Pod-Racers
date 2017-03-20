using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUpdate : MonoBehaviour
{
    public Transform podRacer;
    public ControllerPositionInput cpi;
    public CheckPointManager cpm;
    public RectTransform leftBar, rightBar;
    public RawImage navigator;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Update bars
        leftBar.sizeDelta = new Vector2(20, 200 * cpi.positionInput.left);
        rightBar.sizeDelta = new Vector2(20, 200 * cpi.positionInput.right);

        CheckPoint cp = cpm.activeCheckpoint;

        // Update navigator
        navigator.transform.LookAt(new Vector3(cp.transform.position.x, podRacer.transform.position.y, cp.transform.position.z));
        navigator.transform.rotation *= Quaternion.Euler(90f, 0f, 0f);
        
    }
}
