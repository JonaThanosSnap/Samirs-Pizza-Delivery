using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private float timeRatio;
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField, Range(0, 24)] private float timeOfDay;

    void Update()
    {
        if (preset == null) return;

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime*timeRatio;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay/24.0f);
        }
        else
        {
            UpdateLighting(timeOfDay/24.0f);
        }
    }

    void OnValidate()
    {
        // Sets directionalLight if not already done so
        if (directionalLight != null) return;
        if (RenderSettings.sun != null) directionalLight = RenderSettings.sun;
    }

    void UpdateLighting(float timePercent)
    {
        // timePercent ranges from 0.0 to 1.0
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            // Angle the light
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent*360.0f)-90.0f, 170.0f, 0.0f));
        }
    }
}
