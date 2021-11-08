using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{

    DateTime parkStartTime;
    TimeSpan parkTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        parkStartTime = DateTime.Now;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.attachedRigidbody.velocity.magnitude < 0.5)
        {
            parkTime = DateTime.Now.Subtract(parkStartTime);
            if (parkTime.Seconds > 5)
            {
                Debug.Log("Samir has successfully parked!");
            }

        }
        else
        {
            parkStartTime = DateTime.Now;
        }
    }
}
