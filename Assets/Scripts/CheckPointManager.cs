using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {

    public GameObject[] cp; //holds all check points

    int activeCP = 0;

	// Use this for initialization
	void Start () {
		
        {
            activeCP = 0;

            cp[activeCP].SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
