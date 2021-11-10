using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAnimation : MonoBehaviour
{
    Vector3 frameRotate = new Vector3(0, 0.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(6, -2, 15);
        transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
    }

    void FixedUpdate()
    {
        transform.Rotate(frameRotate);
    }
}
