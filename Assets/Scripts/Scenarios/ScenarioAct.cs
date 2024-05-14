using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScenarioAct : MonoBehaviour
{
    [SerializeField]
    private ActRequirement[] _requirements;
    [SerializeField]
    private ActPhase[] _phases;

    public IEnumerable<ActRequirement> Requirements => _requirements;
    public IEnumerable<ActPhase> Phases => _phases;

    private bool _hasEnded;
    private HashSet<ActPhase> _endedPhases;

    [field: SerializeField]
    public bool IsRepetitive { get; protected set; }

    public bool HasEnded
    {
        get => _hasEnded;
        protected set
        {
            _hasEnded = value;
            if (_hasEnded)
            {
                Ended?.Invoke(this);
            }
        }
    }

    public event Action<ScenarioAct> Ended;

    private void Start()
    {
        _endedPhases = new();
        _phases.Where(x => x != null).ForEach(x => x.Ended += OnPhaseEnded);

        if (_requirements.Length == 0)
        {
            Invoke();
        }
        else
        {
            foreach (var requirement in _requirements)
            {
                requirement.OnFulfilled += OnRequirementFulfilled;
            }
        }
    }

    private void OnPhaseEnded(ActPhase phase)
    {
        _endedPhases.Add(phase);
        if (_phases.All(x => _endedPhases.Contains(x)))
        {
            HasEnded = true;
        }
    }

    private void OnRequirementFulfilled(ActRequirement requirement)
    {
        if ((IsRepetitive || !HasEnded) &&
            _requirements.Except(requirement.Yield()).All(x => x.IsFulfilled()))
        {
            Invoke();
        }
    }

    private void Invoke()
    {
        _phases.ForEach(x => x.Invoke());
    }
}
