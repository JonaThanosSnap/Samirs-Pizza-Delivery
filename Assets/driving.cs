using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driving : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey("q"))
        {
            Vector3 force = transform.forward * speed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude * 0.02));
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 10);
        }
        if (Input.GetKey("s"))
        {
            Vector3 force = -transform.forward * speed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude * 0.02));
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 10);
        }
        if (Input.GetKey("p"))
        {
            Vector3 force = transform.forward * speed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude * 0.02));
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 10);
        }
        if (Input.GetKey("l"))
        {
            Vector3 force = -transform.forward * speed;
            Vector3 startPos = transform.position + (Quaternion.AngleAxis(90, Vector3.up) * transform.forward * (float)(transform.localScale.magnitude * 0.02));
            rb.AddForceAtPosition(force, startPos);
            Debug.DrawLine(startPos, startPos + force * 10);
        }
    }
}
