using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMap : MonoBehaviour
{
    bool mapOpen = false;
    RectTransform tf;
    Camera navCam;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<RectTransform>();
        navCam = GameObject.FindGameObjectWithTag("NavCam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m")) {
            if (mapOpen)
            {
                mapOpen = false;
                tf.anchorMin = new Vector2(0, 1);
                tf.anchorMax = new Vector2(0, 1);
                tf.anchoredPosition = new Vector2(75, -75);
                tf.sizeDelta = new Vector2(150, 150);
                navCam.orthographicSize = 100;

            } else
            {
                mapOpen = true;
                tf.anchorMin = new Vector2(0.5f, 0.5f);
                tf.anchorMax = new Vector2(0.5f, 0.5f);
                tf.anchoredPosition = new Vector2(0, 0);
                tf.sizeDelta = new Vector2(400, 400);
                navCam.orthographicSize = 400;
            }
        }
    }
}
