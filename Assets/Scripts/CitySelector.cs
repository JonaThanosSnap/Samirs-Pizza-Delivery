using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct CityCoordinateTuple {
    public string name;
    public double lat;
    public double lng;
}

public class CitySelector : MonoBehaviour
{
    public CityCoordinateTuple[] cities; // Gets translated into a dictionary
    public GameObject mapObj;
    public GameObject navMapObj;
    private TMP_Dropdown dropdownMenu;
    private Dictionary<string, Vector2d> cityDict;
    private AbstractMap map;
    private AbstractMap navMap;
    private MapAnimation mapAni;

    // Start is called before the first frame update
    void Start()
    {
        dropdownMenu = GetComponent<TMP_Dropdown>();

        // Setup drop down options and cityDict
        cityDict = new Dictionary<string, Vector2d>();
        List<string> cityNames = new List<string>();
        foreach (CityCoordinateTuple cct in cities)
        {
            cityDict.Add(cct.name, new Vector2d(cct.lat, cct.lng));
            cityNames.Add(cct.name);
        }
        dropdownMenu.AddOptions(cityNames);

        // Initialize map refs
        map = mapObj.GetComponent<AbstractMap>();
        mapAni = mapObj.GetComponent<MapAnimation>();
        navMap = navMapObj.GetComponent<AbstractMap>();
    }

    // Update is called once per frame
    void Update() {}

    public void SelectCity()
    {
        string cityName = dropdownMenu.captionText.text;
        map.UpdateMap(cityDict[cityName]);
        navMap.SetCenterLatitudeLongitude(cityDict[cityName]);
        mapAni.RohansCoolFunction3();
    }
}
