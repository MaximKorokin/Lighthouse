using System.Linq;
using UnityEngine;

public abstract class ScenarioAct : MonoBehaviour
{
    private ActRequirement[] _requirements;

    private void Start()
    {
        _requirements = GetComponents<ActRequirement>();

        if (_requirements.Length == 0)
        {
            Act();
        }
        else
        {
            foreach (var requirement in _requirements)
            {
                requirement.OnFulfilled += OnRequirementFulfilled;
            }
        }
    }

    private void OnRequirementFulfilled(ActRequirement requirement)
    {
        if (_requirements.Except(requirement.Yield()).All(x => x.IsFulfilled()))
        {
            Act();
        }
    }

    protected abstract void Act();
}
