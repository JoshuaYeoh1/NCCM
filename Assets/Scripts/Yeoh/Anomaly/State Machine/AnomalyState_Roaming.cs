using UnityEngine;

/////////////////////////////////////////////////////////////////////////////////////////

public class AnomalyState_Roaming : BaseState
{
    public override string Name => "Roaming";

    Anomaly anomaly;

    public AnomalyState_Roaming(AnomalyStateMachine sm)
    {
        anomaly = sm.anomaly;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anomaly.gameObject.name} State: {Name}");

        anomaly.stun.ResetStuns();
        anomaly.exposure.currentExposure=0;

        anomaly.move.StartPatrol();
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
        anomaly.move.StopPatrol();
    }
}
