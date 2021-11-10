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
       

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        enabled = false;
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
