using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NavigatorOnly : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (IsHost)
        {
            this.gameObject.Destroy();
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
