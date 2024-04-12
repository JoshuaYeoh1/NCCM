using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyExposure : MonoBehaviour
{
    public AnomalyType type;

    public float exposeSpeed=1;
    public float maxExposure=10;

    [HideInInspector] public float exposure;

    public void IncreaseExposure()
    {
        if(exposure < maxExposure)
        {
            exposure += exposeSpeed*Time.deltaTime;
        }
        else exposure = maxExposure;
    }
    
    public bool IsExposureMax()
    {
        return exposure >= maxExposure;
    }

    public void CheckAttack()
    {
        if(IsExposureMax())
        {
            if(Singleton.Current.shutterClosed)
            {
                if(type==AnomalyType.Aggresive)
                {
                    EventManager.Current.OnAnomalyAttack(gameObject);

                    exposure=0;

                    Singleton.Current.DamageShutter(1);
                }
                else if(type==AnomalyType.Passive)
                {
                    exposure=0;
                }
            }
            else canJumpscare=true;
            
        }
    }

    [HideInInspector] public bool canJumpscare;
}
