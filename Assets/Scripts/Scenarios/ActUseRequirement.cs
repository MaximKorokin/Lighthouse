using UnityEngine;

public class ActUseRequirement : ActRequirement
{
    [SerializeField]
    private ScenarioAct _scenarioAct;

    private void Awake()
    {
        _scenarioAct.Used += OnUsed;
    }

    private void OnUsed()
    {
        InvokeFulfilled();
    }

    public override bool IsFulfilled()
    {
        return _scenarioAct.IsUsed;
    }

    private void OnDestroy()
    {
        _scenarioAct.Used -= OnUsed;
    }
}
