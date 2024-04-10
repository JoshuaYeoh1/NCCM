using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    TimeManager timeManager;

    void Start()
    {
        timeManager=TimeManager.Current;
    }  

    public TextMeshProUGUI clockTMP;

    void Update()
    {
        clockTMP.text = $"{timeManager.hour} {timeManager.meridiem}";
    }
}
