using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using System;
using TMPro;

public class driving : NetworkBehaviour
{
    Rigidbody rb;
    Transform tf;
    public float maxSpeed;

    public float leftSpeed;
    public float rightSpeed;
    PrefabSelector ps;

    public bool active;
    GameObject stopwatchCanvas;
    TMP_Text countdownTxt;

    public NetworkVariable<Vector3> rbVel = new NetworkVariable<Vector3>();
    public NetworkVariable<int> Score = new NetworkVariable<int>();
    public NetworkVariable<TimeSpan> stopwatch = new NetworkVariable<TimeSpan>();

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PrefabSelector>();
        if (IsHost)
        {
            Score.Value = 0;
        }
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 4f;

        // De-initialize Menu
        GameObject.Find("MenuCam").SetActive(false);
        GameObject.Find("MenuCar").SetActive(false);
        GameObject.Find("MenuCanvas").SetActive(false);
        NetworkManager.Singleton.GetComponent<PlayerHandler>().endCanvas.SetActive(false);

        // Initialize Map
        GameObject map = NetworkManager.Singleton.GetComponent<PlayerHandler>().map;
        map.SetActive(true);
        map.transform.position = Vector3.zero;
        map.transform.localScale = Vector3.one;
        map.GetComponent<MapAnimation>().enabled = false;
        map.transform.rotation = Quaternion.Euler(0, 0, 0);
        /* map.GetComponent<MapExtendsCar>().enabled = true;*/

        map.GetComponent<AbstractMap>().SetExtent(MapExtentType.RangeAroundTransform);
        int buffer = 1;
        map.GetComponent<AbstractMap>().SetExtentOptions(new RangeAroundTransformTileProviderOptions { targetTransform = this.transform, visibleBuffer = buffer, disposeBuffer = buffer });

        GameObject navMap = NetworkManager.Singleton.GetComponent<PlayerHandler>().navMap;
        navMap.GetComponent<AbstractMap>().SetExtent(MapExtentType.RangeAroundTransform);
        buffer = 2;
        navMap.GetComponent<AbstractMap>().SetExtentOptions(new RangeAroundTransformTileProviderOptions { targetTransform = GameObject.FindGameObjectWithTag("Navigator").transform, visibleBuffer = buffer, disposeBuffer = buffer });
        /*navMap.GetComponent<MapExtendsCar>().enabled = true;*/
        navMap.GetComponent<AbstractMap>().Initialize(new Mapbox.Utils.Vector2d(40.7484665, -73.985542), 16);
        navMap.transform.position = new Vector3(0, -100, 0);


        AudioManager audMan = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audMan.StopAll();
        audMan.PlayLooped("racetomtfuji");

        GameObject.Find("DestinationManager").GetComponent<DestinationManager>().CreateDestination();

        stopwatchCanvas = Instantiate(ps.canvasPrefab, Vector3.zero, Quaternion.identity);
        countdownTxt = stopwatchCanvas.GetComponent<SamirWatch>().countdownText;

        active = false;
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int c = seconds;
        while (c > 0)
        {
            countdownTxt.text = c.ToString();
            yield return new WaitForSeconds(1);
            c--;
        }
        active = true;
        countdownTxt.text = "GO!";
        // Initialize Logic
        stopwatchCanvas.GetComponent<SamirWatch>().enabled = true;
        if (!IsHost) GameObject.Find("Minimap").GetComponent<ToggleMap>().enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!active) return;

        // BOTH DRIVER AND NAVIGATOR
        if (countdownTxt.color.a > 0)
        {
            Color c = countdownTxt.color;
            c.a -= 0.02f;
            countdownTxt.color = c;
        }


        if (!IsLocalPlayer) return; // Only look for inputs if the client is the Driver

        rbVel.Value = rb.velocity;
        // DRIVER ONLY
        if (GameLoadParameters.inputMode == InputMode.keyboard)
        {
            if (Input.GetKey("q"))
            {
                Vector3 force = transform.forward * maxSpeed;
                Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
                startPos.y += transform.localScale.magnitude * 0.6f;
                rb.AddForceAtPosition(force, startPos);
                Debug.DrawLine(startPos, startPos + force * 0.001f);
            }
            if (Input.GetKey("s"))
            {
                Vector3 force = -transform.forward * maxSpeed;
                Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
                startPos.y += transform.localScale.magnitude * 0.6f;
                rb.AddForceAtPosition(force, startPos);
                Debug.DrawLine(startPos, startPos + force * 0.001f);
            }
            if (Input.GetKey("p"))
            {
                Vector3 force = transform.forward * maxSpeed;
                Vector3 startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
                startPos.y += transform.localScale.magnitude * 0.6f;
                rb.AddForceAtPosition(force, startPos);
                Debug.DrawLine(startPos, startPos + force * 0.001f);
            }
            if (Input.GetKey("l"))
            {
                Vector3 force = -transform.forward * maxSpeed;
                Vector3 startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
                startPos.y += transform.localScale.magnitude * 0.6f;
                rb.AddForceAtPosition(force, startPos);
                Debug.DrawLine(startPos, startPos + force * 0.001f);
            }
        }

        else if (GameLoadParameters.inputMode == InputMode.arduino)
        {
            // Handle left side
            Vector3 force = transform.forward * leftSpeed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
            startPos.y += transform.localScale.magnitude * 0.6f;
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 0.001f);

            //Handle right side
            force = transform.forward * rightSpeed;
            startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
            startPos.y += transform.localScale.magnitude * 0.6f;
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 0.001f);
        }

        else if (GameLoadParameters.inputMode == InputMode.controller)
        {
            leftSpeed = Input.GetAxis("Left") * maxSpeed;
            rightSpeed = Input.GetAxis("Right") * maxSpeed;

            // Handle left side
            Vector3 force = transform.forward * leftSpeed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
            startPos.y += transform.localScale.magnitude * 0.6f;
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 0.001f);

            //Handle right side
            force = transform.forward * rightSpeed;
            startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
            startPos.y += transform.localScale.magnitude * 0.6f;
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 0.001f);
        }
    }

    public void updateScore(int score)
    {
        Score.Value += score;
    }
}