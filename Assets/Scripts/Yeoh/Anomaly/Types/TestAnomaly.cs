using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnomaly : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.AnomalyJumpscareEvent += OnAnomalyJumpscare;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalyDespawnEvent -= OnAnomalyJumpscare;
    }

    void OnAnomalyJumpscare(GameObject predator)
    {
        if(predator!=gameObject) return;

        EventManager.Current.OnAnomalyTeleportRandom(predator);

        //EventManager.Current.OnAnomalyDespawn(predator);
    }
}
