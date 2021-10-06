using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoMove : MonoBehaviour
{
    public SerialController serialController;
    
    private float AmountToMove;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("Cube").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        string message = serialController.ReadSerialMessage(); 
        if(message != null) {
            float data = float.Parse(message);
            //Debug.Log(message);
            MoveObject(data);
        }
    }

    void MoveObject(float direction) {
        transform.Translate(Vector3.left * 0.01f * direction, Space.World);
    }
}
