using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public AnomalySpawner spawner;

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
            spawner.maxActiveAnomalies=1;

            spawner.anomalies.Add(pocong);
            spawner.anomalies.Add(ponna);
        }
        else if(hour==12)
        {
            spawner.anomalies.Add(toyol);
        }
        else if(hour==1)
        {
            spawner.maxActiveAnomalies=2;
        }
        else if(hour==3)
        {
            spawner.anomalies.Add(minyak);
            spawner.anomalies.Remove(ponna);
        }
        else if(hour==4)
        {
            spawner.anomalies.Add(pretas);
        }
        else if(hour==6)
        {
            spawner.anomalies.Clear();

            spawner.maxActiveAnomalies=0;

            ScenesManager.Current.TransitionTo(Scenes.WinScene);
        }
    }
}
