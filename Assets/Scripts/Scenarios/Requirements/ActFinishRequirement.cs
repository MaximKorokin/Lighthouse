using UnityEngine;

public class ActFinishRequirement : ActRequirement
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
        _scenarioAct.Finished += OnFinished;
    }

    private void OnFinished(ScenarioAct act)
    {
        InvokeFulfilled();
    }

    public override bool IsFulfilled()
    {
        if (_scenarioAct == null)
        {
            return true;
        }
        return _scenarioAct.HasFinished;
    }

    private void OnDestroy()
    {
        if (_scenarioAct == null)
        {
            return;
        }
        _scenarioAct.Finished -= OnFinished;
    }

    public override string IconName => "Transition.png";
}
