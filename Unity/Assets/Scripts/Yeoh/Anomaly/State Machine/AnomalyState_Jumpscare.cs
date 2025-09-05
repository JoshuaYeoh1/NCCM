using UnityEngine;

/////////////////////////////////////////////////////////////////////////////////////////

public class AnomalyState_Jumpscare : BaseState
{
    public override string Name => "Jumpscare";

    Anomaly anomaly;

    public AnomalyState_Jumpscare(AnomalyStateMachine sm)
    {
        anomaly = sm.anomaly;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anomaly.gameObject.name} State: {Name}");

        EventManager.Current.OnAnomalyJumpscare(anomaly.gameObject);
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
