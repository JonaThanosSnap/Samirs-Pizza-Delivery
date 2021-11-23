using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSettings : MonoBehaviour
{
    ParticleSystem.EmissionModule emission;
    driving driving;

    // Start is called before the first frame update
    void Start()
    {
        emission = GetComponent<ParticleSystem>().emission;
        driving = GameObject.FindGameObjectWithTag("Player").GetComponent<driving>();
    }

    // Update is called once per frame
    void Update()
    {
        if (driving.rbVel.Value.magnitude > 0.5f)
        {
            emission.rateOverTime = (int)Mathf.Clamp(driving.rbVel.Value.magnitude * 3, 0, 125);
        } else
        {
            emission.rateOverTime = 0;
        }
    }
}
