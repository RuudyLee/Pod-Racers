using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    public GameObject[] cp; //holds all check points

    int activeCP;

    // Use this for initialization


    void Start()
    {
        if (cp.Length < 0)
        {
            Debug.Log("nothing inside checkpoint group");
        }
        else
        {
            for (int i = 0; i < cp.Length; i++)
            {
                cp[i].SetActive(false);
            }

            activeCP = 0;

            cp[activeCP].SetActive(true);
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

        if (activeCP >= cp.Length)
        {
            activeCP = 0;
        }

        spawnNextCheck();
    }

    public void spawnNextCheck()
    {

        cp[activeCP].SetActive(true);
    }

}
