using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnPreferences : NetworkBehaviour
{
    public PlayerHandler playerHandler;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
    }
}
