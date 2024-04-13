using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnomalyType
{
    Passive,
    Aggresive,
}

public class Anomaly : MonoBehaviour
{
    [HideInInspector] public AnomalyMove move;
    [HideInInspector] public AnomalyStun stun;
    [HideInInspector] public AnomalyExposure exposure;
    
    void Awake()
    {
        move=GetComponent<AnomalyMove>();
        stun=GetComponent<AnomalyStun>();
        exposure=GetComponent<AnomalyExposure>();
    }

    void OnEnable()
    {
        EventManager.Current.AnomalyDespawnEvent += OnAnomalyDespawn;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalyDespawnEvent -= OnAnomalyDespawn;
    }

    void OnAnomalyDespawn(GameObject anomaly)
    {
        if(anomaly!=gameObject) return;

        Destroy(gameObject, .2f);
    }    
}
