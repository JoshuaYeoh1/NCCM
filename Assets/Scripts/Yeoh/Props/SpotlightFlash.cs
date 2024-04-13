using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightFlash : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.ToggleFlashEvent += OnToggleFlash;
    }
    void OnDisable()
    {
        EventManager.Current.ToggleFlashEvent -= OnToggleFlash;
    }

    public GameObject lightFlash;
    
    void OnToggleFlash(bool toggle)
    {
        lightFlash.SetActive(toggle);
    }
}
