using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class DestinationManager : MonoBehaviour
{

    Vector2d destinationLatLong;
    Vector3 destinationWorldPos;
    Transform mapTransform;    

    public GameObject destinationPrefab;
    public AbstractMap map;
    public AbstractMap navMap;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateDestination()
    {
        destinationLatLong = map.CenterLatitudeLongitude;
        //destinationLatLong = new Vector2d(40.750833, -73.9825);
        destinationWorldPos = map.GeoToWorldPosition(destinationLatLong, false);

        mapTransform = map.Root;

        Instantiate(destinationPrefab,destinationWorldPos, Quaternion.identity, mapTransform);

        Debug.Log("Destination Latitude and Longitude: " + destinationLatLong.x.ToString() + ", " + destinationLatLong.y.ToString());
        Debug.Log("Destination Unity World Position: " + destinationWorldPos.x.ToString() + ", " + destinationWorldPos.y.ToString() + ", " + destinationWorldPos.z.ToString());
    }

}
