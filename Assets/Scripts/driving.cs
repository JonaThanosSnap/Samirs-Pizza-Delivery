using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class driving : NetworkBehaviour
{
    Rigidbody rb;
    Transform tf;
    public float maxSpeed;
<<<<<<< HEAD
=======

    public float leftSpeed;
    public float rightSpeed;

    public InputMode inputMode; // Change to auto input switching later
    
>>>>>>> main
    bool isDriver;
    public float leftSpeed;
    public float rightSpeed;
    public InputMode inputMode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 4f;
        if (IsServer) isDriver = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDriver || !IsLocalPlayer) return; // Only look for inputs if the client is the Driver

        if (inputMode == InputMode.keyboard)
        {
            if (Input.GetKey("q"))
            {
<<<<<<< HEAD
                Vector3 force = transform.forward * maxSpeed;
=======
                Vector3 force = transform.forward * leftSpeed;
>>>>>>> main
                Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
                startPos.y += transform.localScale.magnitude * 0.6f;
                rb.AddForceAtPosition(force, startPos);
                Debug.DrawLine(startPos, startPos + force * 0.001f);
            }
            if (Input.GetKey("s"))
            {
<<<<<<< HEAD
                Vector3 force = -transform.forward * maxSpeed;
=======
                Vector3 force = -transform.forward * leftSpeed;
>>>>>>> main
                Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
                startPos.y += transform.localScale.magnitude * 0.6f;
                rb.AddForceAtPosition(force, startPos);
                Debug.DrawLine(startPos, startPos + force * 0.001f);
            }
            if (Input.GetKey("p"))
            {
<<<<<<< HEAD
                Vector3 force = transform.forward * maxSpeed;
=======
                Vector3 force = transform.forward * rightSpeed;
>>>>>>> main
                Vector3 startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
                startPos.y += transform.localScale.magnitude * 0.6f;
                rb.AddForceAtPosition(force, startPos);
                Debug.DrawLine(startPos, startPos + force * 0.001f);
            }
            if (Input.GetKey("l"))
            {
<<<<<<< HEAD
                Vector3 force = -transform.forward * maxSpeed;
=======
                Vector3 force = -transform.forward * rightSpeed;
>>>>>>> main
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