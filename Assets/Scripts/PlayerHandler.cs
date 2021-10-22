using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHandler : NetworkManager
{
    public List<GameObject> connectedClients; //subject to change when we handle multiple rooms

    public override void OnStartServer()
    {
        base.OnStartServer();

        connectedClients = new List<GameObject>();

        NetworkServer.RegisterHandler<netID>(spawnCar);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        netID driver = new netID { isDriver = false };
        if (numPlayers == 0) // subject to change when we handle multiple rooms
        {
            driver = new netID { isDriver = true }; // way to choose driver/navigator through server
        }
        conn.Send(driver);
    }

    public void spawnCar(NetworkConnection conn, netID netid)
    {
        if (numPlayers == 0)
        {
            GameObject car = Instantiate(playerPrefab);

            NetworkServer.AddPlayerForConnection(conn, car);
        } else
        {
            GameObject nav = Instantiate(spawnPrefabs[0]);
            NetworkServer.AddPlayerForConnection(conn, nav);
        }
    }
}

public struct netID : NetworkMessage
{
    public bool isDriver;
}
