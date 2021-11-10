using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Relay;
using System.Threading.Tasks;
using Unity.Services.Relay.Models;
using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class PlayerHandler : NetworkManager
{
    int maxConnections = 2;
    ulong driverId, navId;

    PrefabSelector ps;

    private void Start()
    {
        ps = GetComponent<PrefabSelector>();
        // static variable in another scene for the code, if code is Null or Empty then host, else join
    }

    public IEnumerator HostGame()
    {

        var authTask = Authenticate();
        while (!authTask.IsCompleted) yield return null;
        string pID = authTask.Result;

        var serverRelayUtilityTask = AllocateRelayServerAndGetJoinCode(maxConnections);
        while (!serverRelayUtilityTask.IsCompleted)
        {
            yield return null;
        }
        if (serverRelayUtilityTask.IsFaulted)
        {
            Debug.LogError("Exception thrown when attempting to start Relay Server. Server not started. Exception: " + serverRelayUtilityTask.Exception.Message);
            yield break;
        }

        var (ipv4address, port, allocationIdBytes, connectionData, key, joinCode) = serverRelayUtilityTask.Result;

        // Display the joinCode to the user.
        GameObject.Find("CodeText").GetComponent<Text>().text = joinCode;

        // When starting a Relay server, both instances of connection data are identical.
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(ipv4address, port, allocationIdBytes, key, connectionData);
        NetworkManager.Singleton.StartHost();
        yield return null;
    }

    // For the Host
    public static async Task<(string ipv4address, ushort port, byte[] allocationIdBytes, byte[] connectionData, byte[] key, string joinCode)> AllocateRelayServerAndGetJoinCode(int maxConnections, string region = null)
    {
        Allocation allocation;
        string createJoinCode;
        try
        {
            allocation = await Relay.Instance.CreateAllocationAsync(maxConnections);
        }
        catch (Exception e)
        {
            Debug.LogError($"Relay create allocation request failed {e.Message}");
            throw;
        }

        Debug.Log($"server: {allocation.ConnectionData[0]} {allocation.ConnectionData[1]}");
        Debug.Log($"server: {allocation.AllocationId}");

        try
        {
            createJoinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
        }
        catch
        {
            Debug.LogError("Relay create join code request failed");
            throw;
        }

        return (allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.ConnectionData, allocation.Key, createJoinCode);
    }

    public IEnumerator JoinGame(string RelayJoinCode)
    {
        var authTask = Authenticate();
        while (!authTask.IsCompleted) yield return null;
        string pID = authTask.Result;

        // Populate RelayJoinCode beforehand through the UI
        var clientRelayUtilityTask = JoinRelayServerFromJoinCode(RelayJoinCode);

        while (!clientRelayUtilityTask.IsCompleted)
        {
            yield return null;
        }

        if (clientRelayUtilityTask.IsFaulted)
        {
            Debug.LogError("Exception thrown when attempting to connect to Relay Server. Exception: " + clientRelayUtilityTask.Exception.Message);
            yield break;
        }

        var (ipv4address, port, allocationIdBytes, connectionData, hostConnectionData, key) = clientRelayUtilityTask.Result;



        // When connecting as a client to a Relay server, connectionData and hostConnectionData are different.
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(ipv4address, port, allocationIdBytes, key, connectionData, hostConnectionData);
        NetworkManager.Singleton.StartClient();
        yield return null;
    }

    //For the Client
    public static async Task<(string ipv4address, ushort port, byte[] allocationIdBytes, byte[] connectionData, byte[] hostConnectionData, byte[] key)> JoinRelayServerFromJoinCode(string joinCode)
    {
        JoinAllocation allocation;
        try
        {
            allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
        }
        catch
        {
            Debug.LogError("Relay create join code request failed");
            throw;
        }

        Debug.Log($"client: {allocation.ConnectionData[0]} {allocation.ConnectionData[1]}");
        Debug.Log($"host: {allocation.HostConnectionData[0]} {allocation.HostConnectionData[1]}");
        Debug.Log($"client: {allocation.AllocationId}");

        return (allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.ConnectionData, allocation.HostConnectionData, allocation.Key);
    }

    public async Task<string> Authenticate()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            var playerID = AuthenticationService.Instance.PlayerId;
            return playerID;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    public void StartGame() // Only runs on host
    {


        // Initialize Host
        GameObject d = Instantiate(ps.driverPrefab, new Vector3(70, 30, 0), Quaternion.identity);
        d.GetComponent<NetworkObject>().SpawnAsPlayerObject(driverId);

        // Initialize Client
        GameObject n = Instantiate(ps.navPrefab, Vector3.zero, Quaternion.identity);
        n.GetComponent<NetworkObject>().SpawnAsPlayerObject(navId);

    }
}
