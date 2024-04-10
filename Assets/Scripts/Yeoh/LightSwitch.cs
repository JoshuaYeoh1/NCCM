using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.ClickEvent += OnClick;
    }
    void OnDisable()
    {
        EventManager.Current.ClickEvent -= OnClick;
    }
    
    void OnClick(GameObject target)
    {
        if(target!=gameObject) return;
        
        EventManager.Current.OnToggleLights(!Singleton.Current.lightsOn);
    }
}
