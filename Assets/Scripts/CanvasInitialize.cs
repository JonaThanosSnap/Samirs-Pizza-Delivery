using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInitialize : MonoBehaviour
{
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
