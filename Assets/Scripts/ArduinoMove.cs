using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoMove : MonoBehaviour
{
    public SerialController serialController;
    driving driving;

    private float AmountToMove;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GetComponent<SerialController>();
        driving = GetComponent<driving>();
    }

    // Update is called once per frame
    void Update()
    {
        if (driving.inputMode == InputMode.arduino)
        {
            if (!serialController.enabled) serialController.enabled = false;

            string message = serialController.ReadSerialMessage();
            if (message != null)
            {
                string[] inputString = message.Split('\t');
                Vector2 inputs = new Vector2(float.Parse(inputString[0]), float.Parse(inputString[1]));

                driving.leftSpeed = inputs.x * driving.maxSpeed;
                driving.rightSpeed = inputs.y * driving.maxSpeed;
            }
        } else { serialController.enabled = false; }

    }
}
