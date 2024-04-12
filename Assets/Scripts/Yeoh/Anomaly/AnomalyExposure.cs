using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyExposure : MonoBehaviour
{
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
                EventManager.Current.OnAnomalyAttack(gameObject);

                exposure=0;

                Singleton.Current.shutterHp--;

                if(Singleton.Current.shutterHp<=0)
                {
                    EventManager.Current.OnShutterBreak();
                }
            }
            else canJumpscare=true;
        }
    }

    [HideInInspector] public bool canJumpscare;
}
