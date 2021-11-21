using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    GameObject destinationMarker;
    GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GameObject.FindGameObjectWithTag("MarkerArrow");
    }

    public void SetMarkerObject(GameObject obj)
    {
        destinationMarker = obj;  
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (destinationMarker)
        {
            Vector3 normalizedPos = GetComponent<Camera>().WorldToViewportPoint(destinationMarker.transform.position);
            if (!(0 <= normalizedPos.x && normalizedPos.x <= 1 && 0 <= normalizedPos.y && normalizedPos.y <= 1))
            {
                arrow.SetActive(true);
                Vector2 toMarker = new Vector2(destinationMarker.transform.position.x - transform.position.x, destinationMarker.transform.position.z - transform.position.z);
                toMarker = toMarker.normalized * 30;
                arrow.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(toMarker.x, -51, toMarker.y);
                arrow.transform.rotation = Quaternion.LookRotation(new Vector3(toMarker.x, 0, toMarker.y));
                arrow.transform.Rotate(0, 180, 0);
            }
            else
            {
                arrow.SetActive(false);
            }
        }
    }
}
