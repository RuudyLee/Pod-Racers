using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    CheckPoint[] checkpoints; //holds all check points

    int activeCP;

    // Use this for initialization
    void Start()
    {
        checkpoints = GetComponentsInChildren<CheckPoint>();
        
        
        if (checkpoints.Length < 0)
        {
            Debug.Log("nothing inside checkpoint group");
        }
        else
        {
            foreach (CheckPoint cp in checkpoints)
            {
                cp.gameObject.SetActive(false);
            }

            activeCP = 0;

            checkpoints[activeCP].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(activeCP);

    }

    public void incrActivCP()
    {
        activeCP++;

        if (activeCP >= checkpoints.Length)
        {
            activeCP = 0;
        }

        spawnNextCheck();
    }

    public void spawnNextCheck()
    {

        checkpoints[activeCP].gameObject.SetActive(true);
    }

}
