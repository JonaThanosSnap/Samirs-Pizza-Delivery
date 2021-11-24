using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Unity.Map;

public class MapAnimation : MonoBehaviour
{
    Vector3 frameRotate = new Vector3(0, 0.5f, 0);

    // Start is called before the first frame update
    public void Start()
    {
        RohansCoolFunction3();
    }

    void FixedUpdate()
    {
        transform.Rotate(frameRotate);
    }

    public void RohansCoolFunction3() {
        transform.position = new Vector3(6, -2, 15);
        transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
    }
}
