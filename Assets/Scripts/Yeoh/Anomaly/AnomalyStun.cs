using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyStun : MonoBehaviour
{
    public bool immuneToSound;
    public bool immuneToFlash;
    public bool immuneToCage;
    public bool immuneToDoorLock;
    public bool immuneToLight;

    bool isSoundStunned;
    bool isFlashStunned;
    bool isCageStunned;
    bool isDoorStunned;
    bool isLightStunned;

    [HideInInspector] public int stunCombo;

    public int stunsToExpel=2;

    public void CheckAll()
    {
        CheckStun(immuneToSound, Singleton.Current.soundPitchActive, ref isSoundStunned);
        CheckStun(immuneToFlash, Singleton.Current.flashActive, ref isFlashStunned);
        CheckStun(immuneToCage, Singleton.Current.cageActive, ref isCageStunned);
        CheckStun(immuneToDoorLock, Singleton.Current.doorLocked, ref isDoorStunned);
        CheckStun(immuneToLight, Singleton.Current.lightsOn, ref isLightStunned);
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
    }
}
