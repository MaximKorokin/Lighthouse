using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ScenarioAct : MonoBehaviour, IInitializable<ScenarioAct>
{
    [SerializeField]
    private List<ActRequirement> _requirements;
    [SerializeField]
    private List<ActPhase> _phases;
    [SerializeField]
    private List<ScenarioAct> _childrenActs;

    [field: SerializeField]
    public bool IsRepetitive { get; private set; }

    public IEnumerable<ActRequirement> Requirements => _requirements;
    public IEnumerable<ActPhase> Phases => _phases;
    public IEnumerable<ScenarioAct> ChildrenActs => _childrenActs;

    private bool _hasEnded;
    public bool HasEnded => _hasEnded;

    private HashSet<ActPhase> _endedPhases;

    public event Action<ScenarioAct> Ended;
    public event Action<ScenarioAct> Initialized;

    public void Initialize()
    {
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
        Initialized?.Invoke(this);
    }

    private void End()
    {
        _hasEnded = true;
        _childrenActs.ForEach(x => x.Initialize());
        Ended?.Invoke(this);
    }

    private void Start()
    {
        if (Application.isPlaying && _selfInitializable)
        {
            Initialize();
        }
    }

    private void OnPhaseEnded(ActPhase phase)
    {
        _endedPhases.Add(phase);
        if (_phases.All(x => _endedPhases.Contains(x)))
        {
            End();
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
            End();
        }
        _phases.ForEach(x => x.Invoke());
    }
}
