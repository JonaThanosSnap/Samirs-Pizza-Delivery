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

    public GameObject dest;

    public void CreateDestination()
    {
        if (IsHost)
        {
            Vector3 transformDestinationVertex = GetRandomDestinationTransform();

            dest = Instantiate(destinationPrefab, transformDestinationVertex, Quaternion.identity);
            dest.GetComponent<NetworkObject>().Spawn();
        }
    }


    public void ChangeDestination()
    {
        if (IsHost)
        {
            Vector3 transformDestinationVertex = GetRandomDestinationTransform();

            dest.transform.position = transformDestinationVertex;
        }
    }


    private Vector3 GetRandomDestinationTransform()
    {
        Transform destinationTile = null;
        while (destinationTile == null || destinationTile.gameObject.name == "TileProvider")
        {
            destinationTile = navMap.transform.GetChild(Random.Range(0, navMap.transform.childCount - 1));
        }
        Vector3 transformDestinationVertex = destinationTile.GetChild(Random.Range(0, destinationTile.childCount - 1)).position;
        transformDestinationVertex.y = 0;

        return transformDestinationVertex;
    }
}
