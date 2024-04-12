using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject pocong;
    public GameObject ponna;
    public GameObject toyol;
    public GameObject minyak;
    public GameObject pretas;

    void OnEnable()
    {
        EventManager.Current.HourTickEvent += OnHourTick;
    }
    void OnDisable()
    {
        EventManager.Current.HourTickEvent -= OnHourTick;
    }
    
    void OnHourTick(int hour)
    {
        if(hour==10)
        {
            AnomalySpawner.Current.maxActiveAnomalies=1;

            AnomalySpawner.Current.anomalies.Add(pocong);
            AnomalySpawner.Current.anomalies.Add(ponna);
        }
        else if(hour==12)
        {
            AnomalySpawner.Current.anomalies.Add(toyol);
        }
        else if(hour==1)
        {
            AnomalySpawner.Current.maxActiveAnomalies=2;
        }
        else if(hour==3)
        {
            AnomalySpawner.Current.anomalies.Add(minyak);
            AnomalySpawner.Current.anomalies.Remove(ponna);
        }
        else if(hour==4)
        {
            AnomalySpawner.Current.anomalies.Add(pretas);
        }
    }
}
