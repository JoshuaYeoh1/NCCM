using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        EventManager.Current.AnomalySpawnEvent += OnAnomalySpawn;
        EventManager.Current.AnomalyDespawnEvent += OnAnomalyDespawn;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalySpawnEvent -= OnAnomalySpawn;
        EventManager.Current.AnomalyDespawnEvent -= OnAnomalyDespawn;
    }

    void OnAnomalySpawn(GameObject spawned, Room room, Transform spot)
    {
        if(spawned!=gameObject) return;

        AnomalySpawner.Current.activeAnomalies.Add(gameObject);
    }

    void OnAnomalyDespawn(GameObject anomaly)
    {
        if(anomaly!=gameObject) return;

        AnomalySpawner.Current.activeAnomalies.Remove(gameObject);

        Destroy(gameObject, .1f);
    }    
}
