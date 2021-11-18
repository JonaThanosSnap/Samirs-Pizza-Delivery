using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class MapExtendsCar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject car = GameObject.FindGameObjectWithTag("Player");
        AbstractMap map = GetComponent<AbstractMap>();
        
        map.SetExtent(MapExtentType.RangeAroundTransform);
        int buffer = map.gameObject.name == "NavMap" ? 2 : 1;
        map.SetExtentOptions(new RangeAroundTransformTileProviderOptions{ targetTransform = car.transform, visibleBuffer = buffer, disposeBuffer = buffer});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
