using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManager;

public class MainMenu : MonoBehaviour
{
    public void PlayGame (){

        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");

    }

    public void QuitGame (){

        Debug.Log("Quit");

        Application.Quit();

    }
    
}
