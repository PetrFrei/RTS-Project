using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process : MonoBehaviour
{
    public float quantity;

    public float timeToProcess;


    // Update is called once per frame
    void Update()
    {
        if(quantity>0)
        {
            timeToProcess -= 1*Time.deltaTime;
            if(timeToProcess<=0)
            {
                quantity--;
                Result();
            }
        }
    }

    public virtual void Result()
    {
        
    }
}
