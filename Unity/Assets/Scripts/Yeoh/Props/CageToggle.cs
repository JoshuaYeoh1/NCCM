using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageToggle : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.AnomalyStunEvent += OnAnomalyStun;
        EventManager.Current.AnomalyRecoverEvent += OnAnomalyRecover;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalyStunEvent -= OnAnomalyStun;
        EventManager.Current.AnomalyRecoverEvent -= OnAnomalyRecover;
    }

    public AnomalyStun stun;
    public GameObject cage;
    
    void OnAnomalyStun(GameObject victim)
    {
        if(victim!=stun.gameObject) return;

        cage.SetActive(stun.isCageStunned);
    }

    void OnAnomalyRecover(GameObject subject)
    {
        if(subject!=stun.gameObject) return;

        cage.SetActive(stun.isCageStunned);
    }

    bool cageEnabled;

    void Update()
    {
        if(cage.activeSelf && !cageEnabled)
        {
            cageEnabled=true;
            AudioManager.Current.PlaySFX(SFXManager.Current.sfxCage, transform.position);
        }
        else if(!cage.activeSelf && cageEnabled)
        {
            cageEnabled=false;
            AudioManager.Current.PlaySFX(SFXManager.Current.sfxCageBreak, transform.position);
        }
    }
}
