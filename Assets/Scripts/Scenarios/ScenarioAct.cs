using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class ScenarioAct : MonoBehaviour
{
    [SerializeField]
    private List<ActRequirement> _requirements;
    [SerializeField]
    private List<ActPhase> _phases;

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
        if (!Application.isPlaying)
        {
            return;
        }

        _endedPhases = new();
        _phases.Where(x => x != null).ForEach(x => x.Ended += OnPhaseEnded);

        if (_requirements.Count == 0)
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
        if (_phases == null || _phases.Count == 0)
        {
            HasEnded = true;
        }
        _phases.ForEach(x => x.Invoke());
    }

    // Only for edit mode visualization purposes
    private void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }

        _requirements ??= new();
        _phases ??= new();
        _requirements.Where(x => x == null).ToArray().ForEach(x => _requirements.Remove(x));
        _phases?.Where(x => x == null).ToArray().ForEach(x => _phases.Remove(x));

        foreach (var requirement in GetComponents<ActRequirement>())
        {
            if (!_requirements.Contains(requirement))
            {
                _requirements.Add(requirement);
            }
        }
        foreach (var phase in GetComponents<ActPhase>())
        {
            if (!_phases.Contains(phase))
            {
                _phases.Add(phase);
            }
        }
    }
}
