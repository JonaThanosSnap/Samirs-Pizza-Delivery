using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Destination : NetworkBehaviour
{

    DateTime parkStartTime;
    TimeSpan parkTime;
    float parkProgress = 0f;
    float parkTimeRequired = 3f;
    float parkSpeedRequired = 0.5f;
    public GameObject destWaypoint;
    public GameObject parkWaypoint;
    GameObject car;
    driving driving;

    DateTime lapStartTime;

    public int maxScore;

    DestinationManager destMan;


    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player");
        driving = car.GetComponent<driving>();
        destMan = GameObject.Find("DestinationManager").GetComponent<DestinationManager>();
        GameObject.FindGameObjectWithTag("NavCam").GetComponent<SpawnArrow>().SetMarkerObject(this.gameObject);
        lapStartTime = DateTime.Now;

        maxScore = (int)Vector3.Distance(car.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWaypoints();
    }

    private void OnTriggerEnter(Collider collider)
    {
        parkStartTime = DateTime.Now;

        if (collider.gameObject.name.Contains("Building"))
        {
            destMan.ChangeDestination();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        parkProgress = 0;
    }

    void OnTriggerStay(Collider collider)
    {    
        if (driving.rbVel.Value.magnitude < parkSpeedRequired)
        {
            parkTime = DateTime.Now.Subtract(parkStartTime);

            if (parkTime.Seconds > 3 && IsHost)
            {
                TimeSpan lapElapsedTime = DateTime.Now.Subtract(lapStartTime);
                int score = lapElapsedTime.Seconds / (int)Mathf.Ceil(maxScore / 300.0f);

                driving.stopwatch.Value = GameObject.FindGameObjectWithTag("Stopwatch").GetComponent<SamirWatch>().time;
                driving.updateScore(score);

                destMan.pizzasDelivered++;
                this.gameObject.Destroy();

                if (destMan.pizzasDelivered < destMan.deliveriesRequired)
                {
                    GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("successfuldelivery", false);
                    destMan.CreateDestination();
                }
                else
                {
                    GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().StopAll();
                    NetworkManager.Singleton.GetComponent<PlayerHandler>().EndGame(driving.Score.Value, driving.stopwatch.Value);
                }

            }

            parkProgress = (float)(parkTime.Seconds + parkTime.Milliseconds / 1000.0) / parkTimeRequired;

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

    public override void OnDestroy()
    {
        if (!IsHost)
        {
            destMan.pizzasDelivered++;

            if (destMan.pizzasDelivered < destMan.deliveriesRequired)
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("successfuldelivery", false);
            }
            else
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().StopAll();
                NetworkManager.Singleton.GetComponent<PlayerHandler>().EndGame(driving.Score.Value, driving.stopwatch.Value);
            }
        }
    }
}
