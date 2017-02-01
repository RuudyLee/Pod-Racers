using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUpdate : MonoBehaviour
{
    public ControllerPositionInput cpi;
    public RectTransform leftBar, rightBar;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftBar.sizeDelta = new Vector2(20, 200 * cpi.positionInput.left);
        rightBar.sizeDelta = new Vector2(20, 200 * cpi.positionInput.right);
    }
}
