using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnPreferences : NetworkBehaviour
{
    public PlayerHandler playerHandler;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // TODO move this somewhere appropriate
        // Switch songs
        AudioManager audioManager = GameObject.FindGameObjectsWithTag("AudioManager")[0].GetComponent<AudioManager>();
        audioManager.StopAll();
        audioManager.Play("dk_crash_course", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
    }
}
