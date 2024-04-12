using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockInMachine : MonoBehaviour
{
    bool clockedIn;

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
        
        if(!clockedIn)
        {
            clockedIn=true;

            EventManager.Current.OnClockIn();
        }
    }
}
