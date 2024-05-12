using System;
using System.Linq;
using UnityEngine;

public abstract class ScenarioAct : MonoBehaviour
{
    private ActRequirement[] _requirements;
    private bool _isUsed;

    [field: SerializeField]
    public bool IsReusable { get; protected set; }

    public bool IsUsed
    {
        get => _isUsed;
        protected set
        {
            _isUsed = value;
            if (_isUsed)
            {
                Used?.Invoke();
            }
        }
    }

    public event Action Used;

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
        if ((IsReusable || !IsUsed) && 
            _requirements.Except(requirement.Yield()).All(x => x.IsFulfilled()))
        {
            Act();
        }
    }

    protected abstract void Act();
}
