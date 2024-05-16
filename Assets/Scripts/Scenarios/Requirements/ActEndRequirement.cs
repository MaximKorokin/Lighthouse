using UnityEngine;

public class ActEndRequirement : ActRequirement
{
    [SerializeField]
    private ScenarioAct _scenarioAct;

    public ScenarioAct ScenarioAct => _scenarioAct;

    private void Awake()
    {
        if (_scenarioAct == null)
        {
            Logger.Warn($"{nameof(_scenarioAct)} is null");
            return;
        }
        _scenarioAct.Ended += OnEnded;
    }

    private void OnEnded(ScenarioAct act)
    {
        InvokeFulfilled();
    }

    public override bool IsFulfilled()
    {
        if (_scenarioAct == null)
        {
            return true;
        }
        return _scenarioAct.HasEnded;
    }

    private void OnDestroy()
    {
        if (_scenarioAct == null)
        {
            return;
        }
        _scenarioAct.Ended -= OnEnded;
    }

    //public override string IconName => "Transition.png";
}
