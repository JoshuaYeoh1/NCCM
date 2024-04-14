using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public GameObject clockInPrefab;

    void OnEnable()
    {
        EventManager.Current.ClockInEvent += OnClockIn;
    }
    void OnDisable()
    {
        EventManager.Current.ClockInEvent -= OnClockIn;
    }
    
    void OnClockIn()
    {
        Instantiate(clockInPrefab);
    }

}
