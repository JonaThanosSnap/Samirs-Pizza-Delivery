using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
//using UnityEngine.SceneManager;

public class MainMenu : MonoBehaviour
{

    public void Host (){
        GameLoadParameters.clientType = ClientType.Host;
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }
    public void Join()
    {
        GameLoadParameters.clientType = ClientType.Client;
        GameLoadParameters.joinCode = GameObject.Find("JoinCode").GetComponent<TMP_InputField>().text;
        Regex rgx = new Regex(@"^[6789BCDFGHJKLMNPQRTWbcdfghjklmnpqrtw]{6,12}$");
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }

    public void QuitGame (){

        Debug.Log("Quit");

        Application.Quit();

    }
    
}
