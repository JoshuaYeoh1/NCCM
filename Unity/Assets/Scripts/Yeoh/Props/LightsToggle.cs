using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsToggle : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.ToggleLightsEvent += OnToggleLights;
    }
    void OnDisable()
    {
        EventManager.Current.ToggleLightsEvent -= OnToggleLights;
    }

    void Start()
    {
        OnToggleLights(LevelManager.Current.lightsOn);
    }

    public List<GameObject> lights = new List<GameObject>();
    public Material bulbMaterial;

    void OnToggleLights(bool toggle)
    {
        LevelManager.Current.lightsOn = toggle;

        foreach(GameObject light in lights)
        {
            light.SetActive(toggle);
        }

        bulbMaterial.SetColor("_EmissionColor", toggle ? Color.white : Color.clear);
    }
}
