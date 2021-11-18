using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Unity.Netcode;

public class DestinationManager : NetworkBehaviour
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
        if (IsHost)
        {
            Transform destinationTile = null;
            while (destinationTile == null || destinationTile.gameObject.name == "TileProvider")
            {
                destinationTile = navMap.transform.GetChild(Random.Range(0, navMap.transform.childCount - 1));
            }
            Vector3 transformDestinationVertex = destinationTile.GetChild(Random.Range(0, destinationTile.childCount - 1)).position;
            transformDestinationVertex.y = 4;

            GameObject dest = Instantiate(destinationPrefab, transformDestinationVertex, Quaternion.identity, mapTransform);
            dest.GetComponent<NetworkObject>().Spawn();

            Debug.Log("Destination Latitude and Longitude: " + destinationLatLong.x.ToString() + ", " + destinationLatLong.y.ToString());
            Debug.Log("Destination Unity World Position: " + destinationWorldPos.x.ToString() + ", " + destinationWorldPos.y.ToString() + ", " + destinationWorldPos.z.ToString());
        }
    }

}
