using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {


    CheckPointManager cpManager; 

	// Use this for initialization
	void Start () {
        cpManager = gameObject.GetComponentInParent<CheckPointManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider player)
    {

        if(player.tag == "Player")
        {
            cpManager.incrActivCP();

            //cpManager.GetComponent<CheckPointManager>().spawnNextCheck();

            gameObject.SetActive(false);

        }


    }

}
