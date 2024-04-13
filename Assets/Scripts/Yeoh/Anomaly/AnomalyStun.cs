using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyStun : MonoBehaviour
{
    Anomaly anomaly;

    public bool immuneToSound;
    public bool immuneToFlash;
    public bool immuneToCage;
    public bool immuneToDoorLock;
    public bool immuneToLight;
    public bool immuneToDark;

    [HideInInspector] public bool isSoundStunned;
    [HideInInspector] public bool isFlashStunned;
    [HideInInspector] public bool isCageStunned;
    [HideInInspector] public bool isDoorStunned;
    [HideInInspector] public bool isLightStunned;
    [HideInInspector] public bool isDarkStunned;

    [HideInInspector] public int stunCombo;

    public int stunsToExpel=2;

    void Awake()
    {
        anomaly=GetComponent<Anomaly>();
    }

    public void CheckAll()
    {
        CheckStun(immuneToSound, LevelManager.Current.soundPitchActive, ref isSoundStunned);
        CheckStun(immuneToFlash, LevelManager.Current.flashActive, ref isFlashStunned);
        CheckStun(immuneToCage, LevelManager.Current.cageActive, ref isCageStunned);
        CheckStun(immuneToDoorLock, LevelManager.Current.doorLocked, ref isDoorStunned);
        CheckStun(immuneToLight, LevelManager.Current.lightsOn, ref isLightStunned);
        CheckStun(immuneToDark, !LevelManager.Current.lightsOn, ref isDarkStunned);
    }

    void CheckStun(bool isImmune, bool isDefenseActive, ref bool isStunned)
    {
        if(isImmune) return;

        if(isDefenseActive)
        {
            if(!isStunned)
            {
                isStunned=true;
                stunCombo++;

                EventManager.Current.OnAnomalyStun(gameObject);
            }
        }
        else
        {
            if(isStunned)
            {
                isStunned=false;
                stunCombo--;

                EventManager.Current.OnAnomalyRecover(gameObject);
            }
        }
    }
    
    public bool CanExpel()
    {
        if(stunCombo >= stunsToExpel)
        {
            return true;
        }
        return false;
    }

    public void ResetStuns()
    {
        stunCombo=0;

        isSoundStunned=false;
        isFlashStunned=false;
        isCageStunned=false;
        isDoorStunned=false;
        isLightStunned=false;
        isDarkStunned=false;

        EventManager.Current.OnAnomalyRecover(gameObject);
    }
}
