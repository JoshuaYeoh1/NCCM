using UnityEngine;

/////////////////////////////////////////////////////////////////////////////////////////

public class AnomalyState_Exposing : BaseState
{
    public override string Name => "Exposing";

    Anomaly anomaly;

    public AnomalyState_Exposing(AnomalyStateMachine sm)
    {
        anomaly = sm.anomaly;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anomaly.gameObject.name} State: {Name}");

        anomaly.stun.ResetStuns();
    }

    protected override void OnUpdate(float deltaTime)
    {
        anomaly.stun.CheckAll();

        anomaly.exposure.IncreaseExposure();

        anomaly.exposure.CheckAttack();
    }

    protected override void OnExit()
    {
    }
}
