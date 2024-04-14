using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyStateMachine : MonoBehaviour
{
    StateMachine sm;

    [HideInInspector] public Anomaly anomaly;

    void Awake()
    {
        sm = new StateMachine();

        anomaly=GetComponent<Anomaly>();

        // 1. Create all possible states        
        var roamingState = new AnomalyState_Roaming(this);
        var exposingState = new AnomalyState_Exposing(this);
        var stunnedState = new AnomalyState_Stunned(this);
        var jumpscareState = new AnomalyState_Jumpscare(this);

        // 2. Set all transitions
        /////////////////////////////////////////////////////////////////////////////////////////

        roamingState.AddTransition(exposingState, (timeInState) =>
        {
            if(anomaly.move.IsAtWindow())
            {
                return true;
            }
            return false;
        });

        /////////////////////////////////////////////////////////////////////////////////////////

        // exposingState.AddTransition(stunnedState, (timeInState) =>
        // {
        //     if(anomaly.stun.stunCombo>0)
        //     {
        //         return true;
        //     }
        //     return false;
        // });

        exposingState.AddTransition(roamingState, (timeInState) =>
        {
            if(anomaly.stun.CanExpel())
            {
                EventManager.Current.OnAnomalyExpel(anomaly.gameObject);

                return true;
            }
            else if(anomaly.exposure.attackedShutter)
            {
                EventManager.Current.OnAnomalyExpel(anomaly.gameObject);

                anomaly.exposure.ResetAll();

                return true;
            }
            return false;
        });

        exposingState.AddTransition(jumpscareState, (timeInState) =>
        {
            if(anomaly.exposure.canJumpscare)
            {
                return true;
            }
            return false;
        });

        /////////////////////////////////////////////////////////////////////////////////////////

        // stunnedState.AddTransition(exposingState, (timeInState) =>
        // {
        //     if(anomaly.stun.stunCombo<=0)
        //     {
        //         return true;
        //     }
        //     return false;
        // });

        /////////////////////////////////////////////////////////////////////////////////////////

        jumpscareState.AddTransition(roamingState, (timeInState) =>
        {
            if(!anomaly.exposure.canJumpscare)
            {
                return true;
            }
            return false;
        });

        /////////////////////////////////////////////////////////////////////////////////////////
        
        sm.SetInitialState(roamingState);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////

    void Update()
    {
        sm.Tick(Time.deltaTime);
    }
}
