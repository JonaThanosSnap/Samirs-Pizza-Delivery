using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Destination : NetworkBehaviour
{

    DateTime parkStartTime;
    TimeSpan parkTime;
    float parkProgress = 0;
    float parkTimeRequired = 3;
    float parkSpeedRequired = (float) 0.5;
    public GameObject destWaypoint;
    public GameObject parkWaypoint;

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
        UpdateWaypoints();
    }

    private void OnTriggerEnter(Collider collider)
    {
        parkStartTime = DateTime.Now;
    }

    private void OnTriggerExit(Collider collider)
    {
        parkProgress = 0;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.attachedRigidbody.velocity.magnitude < parkSpeedRequired)
        {
            parkTime = DateTime.Now.Subtract(parkStartTime);
<<<<<<< HEAD
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

=======
            parkProgress = (float) (parkTime.Seconds + parkTime.Milliseconds / 1000.0) / parkTimeRequired;
>>>>>>> 73f9ae7b4baeb508e066a6f255d98a76ea6e4a92
        }
        else
        {
            parkStartTime = DateTime.Now;
            parkProgress = 0;
        }

    }


    void UpdateWaypoints()
    {
        if(parkProgress == 0)
        {
            destWaypoint.SetActive(true);
            parkWaypoint.SetActive(false);
        }
        else if (0 < parkProgress && parkProgress < 1)
        {
            destWaypoint.SetActive(true);
            parkWaypoint.SetActive(true);
        }
        else
        {
            destWaypoint.SetActive(false);
            parkWaypoint.SetActive(true);
            parkProgress = 1;
        }

        destWaypoint.transform.localPosition = new Vector3(0, 1 + parkProgress, 0);
        parkWaypoint.transform.localPosition = new Vector3(0, parkProgress, 0);
        destWaypoint.transform.localScale = new Vector3(1, 1 - parkProgress, 1);
        parkWaypoint.transform.localScale = new Vector3(1, parkProgress, 1);
    }

}
