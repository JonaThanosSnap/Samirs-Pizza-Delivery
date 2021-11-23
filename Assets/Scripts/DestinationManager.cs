using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Unity.Netcode;

public class DestinationManager : NetworkBehaviour
{
    Transform mapTransform;

    public float pizzasDelivered = 0;

    public GameObject destinationPrefab;
    public AbstractMap navMap;

    public int deliveriesRequired;


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
            transformDestinationVertex.y = 0;

            GameObject dest = Instantiate(destinationPrefab, transformDestinationVertex, Quaternion.identity);
            dest.GetComponent<NetworkObject>().Spawn();
        }
    }

}
