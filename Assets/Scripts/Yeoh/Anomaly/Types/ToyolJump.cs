using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyolJump : MonoBehaviour
{
    public JumpTween jump;

    void OnEnable()
    {
        EventManager.Current.AnomalyEnterExposingEvent += OnAnomalyEnterExposing;
        EventManager.Current.AnomalyExitExposingEvent += OnAnomalyExitExposing;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalyEnterExposingEvent -= OnAnomalyEnterExposing;
        EventManager.Current.AnomalyExitExposingEvent -= OnAnomalyExitExposing;
    }
    
    void OnAnomalyEnterExposing(GameObject anomaly)
    {
        if(anomaly!=gameObject) return;

        if(jumpingRt!=null) StopCoroutine(jumpingRt);
        jumpingRt = StartCoroutine(Jumping());
    }

    void OnAnomalyExitExposing(GameObject anomaly)
    {
        if(anomaly!=gameObject) return;

        if(jumpingRt!=null) StopCoroutine(jumpingRt);
    }
    
    public float jumpChance=33;

    Coroutine jumpingRt;

    IEnumerator Jumping()
    {
        while(true)
        {
            yield return new WaitForSeconds(.4f);

            if(Random.Range(0, 100f) <= jumpChance)
            {
                jump.Jump(5);
            }
        }
    }
}
