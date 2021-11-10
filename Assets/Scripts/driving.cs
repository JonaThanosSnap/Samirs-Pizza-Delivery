using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class driving : NetworkBehaviour
{
    Rigidbody rb;
    Transform tf;
    public float maxSpeed;

    public float leftSpeed;
    public float rightSpeed;
    PrefabSelector ps;

    public InputMode inputMode; // Change to auto input switching later

    public bool active;
    GameObject stopwatchCanvas;
    Text countdownTxt;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PrefabSelector>();

        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 4f;

        // De-initialize Menu
        GameObject.Find("MenuCam").Destroy();
        GameObject.Find("MenuCar").Destroy();
        GameObject.Find("MenuCanvas").Destroy();

        // Initialize Map
        GameObject.FindGameObjectWithTag("Map").transform.position = Vector3.zero;
        GameObject.FindGameObjectWithTag("Map").transform.localScale = Vector3.one;
        GameObject.FindGameObjectWithTag("Map").GetComponent<MapAnimation>().enabled = false;
        GameObject.FindGameObjectWithTag("Map").transform.rotation = Quaternion.Euler(0, 0, 0);

        stopwatchCanvas = Instantiate(ps.canvasPrefab, Vector3.zero, Quaternion.identity);
        countdownTxt = stopwatchCanvas.GetComponent<SamirWatch>().countdownText.GetComponent<Text>();

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

        // DRIVER ONLY
        if (inputMode == InputMode.keyboard)
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

        else if (inputMode == InputMode.arduino)
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
    }
}

public enum InputMode
{
    keyboard,
    arduino,
    controller
}