using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterButton : MonoBehaviour
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
        
        EventManager.Current.OnToggleShutter(!LevelManager.Current.shutterClosed);
    }
}
