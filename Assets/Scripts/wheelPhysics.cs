using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelPhysics : MonoBehaviour
{
    WheelCollider wc;

    public WheelType wheelType;
    public float maxTorque;
    public float maxBrakeTorque;

    // Start is called before the first frame update
    void Start()
    {
        wc = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}

public enum WheelType
{
    FrontLeft,
    FrontRight,
    BackLeft,
    BackRight
}