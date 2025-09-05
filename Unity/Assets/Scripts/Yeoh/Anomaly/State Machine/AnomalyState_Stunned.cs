using UnityEngine;

/////////////////////////////////////////////////////////////////////////////////////////

public class AnomalyState_Stunned : BaseState
{
    public override string Name => "Stunned";

    Anomaly anomaly;

    public AnomalyState_Stunned(AnomalyStateMachine sm)
    {
        anomaly = sm.anomaly;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anomaly.gameObject.name} State: {Name}");
    }

    protected override void OnUpdate(float deltaTime)
    {
        anomaly.stun.CheckAll();
    }

    protected override void OnExit()
    {
    }
}
