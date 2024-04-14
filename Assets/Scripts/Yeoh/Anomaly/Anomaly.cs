using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureType
{
    Monster,
    Anthropoid,
    Ghost,
}

public enum AnomalyType
{
    Passive,
    Aggresive,
}

public class Anomaly : MonoBehaviour
{
    public CreatureType creatureType;
    public AnomalyType type;

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

        Destroy(gameObject, .1f);
    }    
}
