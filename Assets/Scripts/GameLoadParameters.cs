using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLoadParameters
{
    public static ClientType clientType { get; set; }
    public static string joinCode { get; set; }

    public static string errorMessage { get; set; } // Used to display error messages to the user
}

public enum ClientType
{
    Host,
    Client
}