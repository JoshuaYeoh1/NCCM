using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyExposure : MonoBehaviour
{
    Anomaly anomaly;

    public AnomalyType type;

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
        RandomizeMaxExposure();
    }

    void RandomizeMaxExposure()
    {
        currentMaxExposure = Random.Range(maxExposure.x, maxExposure.y);
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

    public void CheckAttack()
    {
        if(IsExposureMax())
        {
            if(LevelManager.Current.shutterClosed)
            {
                attackedShutter=true;

                EventManager.Current.OnAnomalyAttack(gameObject, damage);

                currentExposure=0;

                RandomizeMaxExposure();
            }
            else canJumpscare=true;
        }
    }

    [HideInInspector] public bool attackedShutter;
    [HideInInspector] public bool canJumpscare;
}
