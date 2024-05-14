using UnityEngine;

public class ActEndRequirement : ActRequirement
{
    [SerializeField]
    private ScenarioAct _scenarioAct;

    public ScenarioAct ScenarioAct => _scenarioAct;

    private void Awake()
    {
        _scenarioAct.Ended += OnEnded;
    }

    private void OnEnded(ScenarioAct act)
    {
        InvokeFulfilled();
    }

    public override bool IsFulfilled()
    {
        return _scenarioAct.HasEnded;
    }

    private void OnDestroy()
    {
        _scenarioAct.Ended -= OnEnded;
    }

    //public override string IconName => "Transition.png";
}
