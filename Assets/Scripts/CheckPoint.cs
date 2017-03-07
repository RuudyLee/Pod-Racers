using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {


    public GameObject cpManager; 

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider player)
    {

        if(player.tag == "Player")
        {
            cpManager.GetComponent<CheckPointManager>().incrActivCP();

            //cpManager.GetComponent<CheckPointManager>().spawnNextCheck();

            gameObject.SetActive(false);

        }


    }

}
