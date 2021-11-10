using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TempNetworkHud : MonoBehaviour
{
    PlayerHandler nm;

    string code = "code";

    // Start is called before the first frame update
    private void Start()
    {
        nm = NetworkManager.Singleton.GetComponent<PlayerHandler>();
    }

    void OnGUI()
    {
        
        code = GUI.TextField(new Rect(50, 30, 40, 20), code);

        if (GUI.Button(new Rect(5, 5, 40, 20), "Host"))
        {
            StartCoroutine(nm.HostGame());
        }
        if (GUI.Button(new Rect(5, 30, 40, 20), "Client"))
        {
            StartCoroutine(nm.JoinGame(code));
        }
    }
}
