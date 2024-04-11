using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly : MonoBehaviour
{
    public AnomalyTeleport tp;
    public SpriteBillboard billboard;

    void Start()
    {
        AnomalySpawner.Current.activeAnomalies.Add(gameObject);
    }

    void OnDestroy()
    {
        AnomalySpawner.Current.activeAnomalies.Remove(gameObject);
    }
}
