using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Destination : NetworkBehaviour
{

    DateTime parkStartTime;
    TimeSpan parkTime;

    DestinationManager destMan;


    // Start is called before the first frame update
    void Start()
    {
        destMan = GameObject.Find("DestinationManager").GetComponent<DestinationManager>();
        GameObject.FindGameObjectWithTag("NavCam").GetComponent<SpawnArrow>().SetMarkerObject(this.gameObject);
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
            if (parkTime.Seconds > 3)
            {
                destMan.pizzasDelivered++;
                if (destMan.pizzasDelivered != 10)
                {
                    GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("successfuldelivery", false);
                    if (IsHost)
                    {
                        destMan.CreateDestination();
                        this.gameObject.Destroy();
                    }
                } else
                {
                    // EndGame();
                }
                
            }

        }
        else
        {
            parkStartTime = DateTime.Now;
        }
    }
}
