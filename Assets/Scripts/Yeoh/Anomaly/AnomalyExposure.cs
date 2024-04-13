using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyExposure : MonoBehaviour
{
    public AnomalyType type;

    public float exposeSpeed=1;
    public Vector2 maxExposure = new Vector2(8, 12);

    float currentMaxExposure;
    [HideInInspector] public float currentExposure;

    [Range(0,3)]
    public float damage=1;

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
                EventManager.Current.OnAnomalyAttack(gameObject, damage);

                currentExposure=0;

                RandomizeMaxExposure();
            }
            else canJumpscare=true;
        }
    }

    [HideInInspector] public bool canJumpscare;
}
