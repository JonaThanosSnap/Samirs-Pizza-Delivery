using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SamirWatch : MonoBehaviour
{
    float currentTime;
    TimeSpan time;
    public Text currentTimeText;
    public Text countdownText;

    public SerialController serialController;

    IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        enabled = false;
        serialController = GameObject.FindGameObjectWithTag("Player").GetComponent<SerialController>();
        coroutine = updateLCD(0.05f);
        StartCoroutine(coroutine);
    }

    IEnumerator updateLCD(float waitTime) {
        while(true) {
            yield return new WaitForSeconds(waitTime);
            if (!serialController.enabled)
            {
                serialController.enabled = true;
            }
            else
            {
                serialController.SendSerialMessage(currentTimeText.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;
        currentTime = currentTime + Time.deltaTime;
        time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }
}
