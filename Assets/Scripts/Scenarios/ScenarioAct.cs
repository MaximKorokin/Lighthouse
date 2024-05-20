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
    public bool IsConsecutive { get; private set; }
    [field: SerializeField]
    public bool IsRepetitive { get; private set; }

    public IEnumerable<ActRequirement> Requirements => _requirements;
    public IEnumerable<ActPhase> Phases => _phases;
    public IEnumerable<ScenarioAct> ChildrenActs => _childrenActs;

    private PhasesInvoker _invoker;
    private bool _hasEnded;
    public bool HasEnded => _hasEnded;

    public event Action<ScenarioAct> Ended;
    public event Action<ScenarioAct> Initialized;

    public void Initialize()
    {
        _invoker = IsConsecutive ? new ConsecutivePhasesInvoker(_phases) : new SimultaneousPhasesInvoker(_phases);
        _invoker.Ended += OnEnded;

        if (_requirements.Count == 0)
        {
            _invoker.Invoke();
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

    private void OnEnded()
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

    private void OnRequirementFulfilled(ActRequirement requirement)
    {
        if ((IsRepetitive || !HasEnded) &&
            _requirements.Except(requirement.Yield()).All(x => x.IsFulfilled()))
        {
            _invoker.Invoke();
        }
    }

    #region PhasesInvoker
    private abstract class PhasesInvoker
    {
        protected List<ActPhase> Phases;

        public event Action Ended;

        public PhasesInvoker(List<ActPhase> phases)
        {
            Phases = phases;
            Phases.Where(x => x != null).ForEach(x => x.Ended += OnPhaseEnded);
        }

        protected void InvokeEnded()
        {
            Ended?.Invoke();
        }

        public void Invoke()
        {
            if (Phases == null || Phases.Count == 0)
            {
                InvokeEnded();
                return;
            }
            InvokeInternal();
        }

        protected abstract void InvokeInternal();
        protected abstract void OnPhaseEnded(ActPhase phase);
    }

    private class ConsecutivePhasesInvoker : PhasesInvoker
    {
        private int _activePhaseIndex = -1;

        public ConsecutivePhasesInvoker(List<ActPhase> _phases) : base(_phases) { }

        protected override void InvokeInternal()
        {
            _activePhaseIndex++;
            if (_activePhaseIndex < Phases.Count)
            {
                Phases[_activePhaseIndex].Invoke();
            }
        }

        protected override void OnPhaseEnded(ActPhase phase)
        {
            if (_activePhaseIndex + 1 >= Phases.Count)
            {
                _activePhaseIndex = -1;
                InvokeEnded();
                return;
            }
            InvokeInternal();
        }
    }

    private class SimultaneousPhasesInvoker : PhasesInvoker
    {
        private readonly HashSet<ActPhase> _endedPhases = new();

        public SimultaneousPhasesInvoker(List<ActPhase> _phases) : base(_phases) { }

        protected override void InvokeInternal()
        {
            Phases.ForEach(x => x.Invoke());
        }

        protected override void OnPhaseEnded(ActPhase phase)
        {
            _endedPhases.Add(phase);
            if (Phases.All(x => _endedPhases.Contains(x)))
            {
                InvokeEnded();
            }
        }
    }
    #endregion
}
