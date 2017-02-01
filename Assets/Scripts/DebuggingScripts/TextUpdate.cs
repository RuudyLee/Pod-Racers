using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextUpdate : MonoBehaviour {
    public GameObject origin;
    Text m_text;
    
	// Use this for initialization
	void Start () {
        m_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        string val = origin.GetComponent<ControllerPositionInput>().positionInput.left.ToString();
        m_text.text = val.Substring(0, val.IndexOf(".") + 4);
	}
}
