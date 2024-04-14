using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnomaly : MonoBehaviour
{
    Anomaly anomaly;

    void Awake()
    {
        anomaly=GetComponent<Anomaly>();
    }

    public GameObject jumpscarePrefab;

    void OnEnable()
    {
        EventManager.Current.AnomalyJumpscareEvent += OnAnomalyJumpscare;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalyJumpscareEvent -= OnAnomalyJumpscare;
    }

    void OnAnomalyJumpscare(GameObject predator)
    {
        if(predator!=gameObject) return;

        Instantiate(jumpscarePrefab);

        CameraManager.Current.Shake(.6f);

        AudioManager.Current.PlaySFX(SFXManager.Current.sfxJumpscare, transform.position, false);

        if(anomaly.type==AnomalyType.Passive)
        {
            EventManager.Current.OnAnomalyAlertOther(predator);
        }

        EventManager.Current.OnAnomalyDespawn(predator);
    }
}
