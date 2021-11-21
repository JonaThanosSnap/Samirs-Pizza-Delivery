using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarSFX : MonoBehaviour
{
    string audioClip = "int_idle";
    int audioSourceID;
    AudioManager audioManager;
    Rigidbody rb;
    driving driving;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody>();
        audioSourceID = audioManager.Play(audioClip, true);
    }

    // Update is called once per frame
    void Update()
    {
        string prevAudioClip = audioClip;

        if (GameLoadParameters.inputMode == InputMode.keyboard)
        {
            if (Input.GetKey("q") || Input.GetKey("p") || Input.GetKey("s") || Input.GetKey("l"))
            {
                SetAudioClip_EngineOn();
            }
            else
            {
                SetAudioClip_EngineOff();
            }
        }

        else if (GameLoadParameters.inputMode == InputMode.arduino)
        {
            if (Mathf.Abs(Input.GetAxis("Left")) > 0.02f || Mathf.Abs(Input.GetAxis("Right")) > 0.02f)
            {
                SetAudioClip_EngineOn();
            }
            else
            {
                SetAudioClip_EngineOff();
            }
        }

        else if (GameLoadParameters.inputMode == InputMode.controller)
        {
            if (Mathf.Abs(driving.leftSpeed) > 0 || Mathf.Abs(driving.rightSpeed) > 0)
            {
                SetAudioClip_EngineOn();
            }
            else
            {
                SetAudioClip_EngineOff();
            }
        }

        if (prevAudioClip != audioClip) {
            audioManager.Stop(audioSourceID);
            audioSourceID = audioManager.Play(audioClip, true);
        }
    }

    void SetAudioClip_EngineOn()
    {
        float speed = Mathf.Abs(rb.velocity.magnitude);
        if (speed < 25.0) {
            audioClip = "int_low_on";
        } else if (speed < 37.0) {
            audioClip = "int_med_on";
        } else {
            audioClip = "int_high_on";
        }
    }

    void SetAudioClip_EngineOff()
    {
        float speed = Mathf.Abs(rb.velocity.magnitude);
        if (speed < 2.0) {
            audioClip = "int_idle";
        } else if (speed < 25.0) {
            audioClip = "int_low_off";
        } else if (speed < 37.0) {
            audioClip = "int_med_off";
        } else {
            audioClip = "int_high_off";
        }
    }
}
