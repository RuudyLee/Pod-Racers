using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextUpdate : MonoBehaviour {
    public ControllerPositionInput cpi;
    public Text leftText;
    public Text rightText;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        leftText.text = cpi.positionInput.left.ToString();
        rightText.text = cpi.positionInput.right.ToString();
	}
}
