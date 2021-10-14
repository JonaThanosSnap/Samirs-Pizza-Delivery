using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class driving : NetworkBehaviour
{
    Rigidbody rb;
    Transform tf;
    public float speed;
    bool isDriver;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 4f;
        if (isServer) isDriver = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDriver || !isLocalPlayer) return; // Only look for inputs if the client is the Driver

        if (Input.GetKey("q"))
        {
            Vector3 force = transform.forward * speed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
            startPos.y += transform.localScale.magnitude * 0.6f;
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 0.001f);
        }
        if (Input.GetKey("s"))
        {
            Vector3 force = -transform.forward * speed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
            startPos.y += transform.localScale.magnitude * 0.6f;
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 0.001f);
        }
        if (Input.GetKey("p"))
        {
            Vector3 force = transform.forward * speed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
            startPos.y += transform.localScale.magnitude * 0.6f;
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 0.001f);
        }
        if (Input.GetKey("l"))
        {
            Vector3 force = -transform.forward * speed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude));
            startPos.y += transform.localScale.magnitude * 0.6f;
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 0.001f);
        }
    }
}
