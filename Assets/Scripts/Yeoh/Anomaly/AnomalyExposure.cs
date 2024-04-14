using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyExposure : MonoBehaviour
{
    Anomaly anomaly;

    public Vector2 maxExposure = new Vector2(8, 12);
    float currentMaxExposure;

    public float currentExposure;

    [Range(0,3)]
    public float damage=1;

    void Awake()
    {
        anomaly=GetComponent<Anomaly>();
    }

    void Start()
    {
        ResetAll();
    }

    public void ResetAll()
    {
        currentExposure=0;

        currentMaxExposure = Random.Range(maxExposure.x, maxExposure.y);

        attackedShutter=false;

        canJumpscare=false;
    }

    public void IncreaseExposure()
    {
        if(currentExposure < currentMaxExposure)
        {
            float exposeSpeed = 1f-((float)anomaly.stun.stunCombo/anomaly.stun.stunsToExpel);

            currentExposure += exposeSpeed*Time.deltaTime;
        }
        else currentExposure = currentMaxExposure;
    }
    
    public bool IsExposureMax()
    {
        return currentExposure >= currentMaxExposure;
    }

    [HideInInspector] public bool attackedShutter;
    [HideInInspector] public bool canJumpscare;

    public void CheckAttack()
    {
        if(!IsExposureMax()) return;
        
        if(LevelManager.Current.shutterClosed)
        {
            attackedShutter=true;

            EventManager.Current.OnAnomalyAttack(gameObject, damage);
        }
        else canJumpscare=true;
    }
    
}
