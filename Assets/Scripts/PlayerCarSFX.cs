using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarSFX : MonoBehaviour
{
    string audioClip = "int_idle";
    int audioSourceID;
    AudioManager audioManager;
    driving driving;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        driving = GetComponent<driving>();
        audioSourceID = audioManager.Play(audioClip, true);
    }

    // Update is called once per frame
    void Update()
    {
        string prevAudioClip = audioClip;
        if (driving.rbVel.Value.magnitude > 0.5f)
        {
            SetAudioClip_EngineOn();
        }
        else
        {
            SetAudioClip_EngineOff();
        }

        if (prevAudioClip != audioClip) {
            audioManager.Stop(audioSourceID);
            audioSourceID = audioManager.Play(audioClip, true);
        }
    }

    void SetAudioClip_EngineOn()
    {
        float speed = Mathf.Abs(driving.rbVel.Value.magnitude);
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
        float speed = Mathf.Abs(driving.rbVel.Value.magnitude);
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
