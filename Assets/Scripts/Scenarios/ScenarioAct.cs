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
    private bool _hasInitialized;
    private bool _hasFinished;
    public bool HasFinished => _hasFinished;

    public event Action<ScenarioAct> Finished;
    public event Action<ScenarioAct> Initialized;

    public void Initialize()
    {
        if (_hasInitialized)
        {
            return;
        }
        _hasInitialized = true;

        _invoker = IsConsecutive ? new ConsecutivePhasesInvoker(_phases) : new SimultaneousPhasesInvoker(_phases);
        _invoker.Finished += OnFinished;

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

    private void OnFinished()
    {
        _hasFinished = true;
        _childrenActs.ForEach(x => x.Initialize());
        Finished?.Invoke(this);
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
        if ((IsRepetitive || !HasFinished) &&
            _requirements.Except(requirement.Yield()).All(x => x.IsFulfilled()))
        {
            _invoker.Invoke();
        }
    }

    #region PhasesInvoker
    private abstract class PhasesInvoker
    {
        protected List<ActPhase> Phases;

        private bool _isInvoking;

        public event Action Finished;

        public PhasesInvoker(List<ActPhase> phases)
        {
            Phases = phases;
            Phases.Where(x => x != null).ForEach(x => x.Finished += OnPhaseFinished);
        }

        protected void InvokeFinished()
        {
            _isInvoking = false;
            Finished?.Invoke();
        }

        public void Invoke()
        {
            if (_isInvoking)
            {
                return;
            }
            _isInvoking = true;
            if (Phases == null || Phases.Count == 0)
            {
                InvokeFinished();
                return;
            }
            InvokeInternal();
        }

        protected abstract void InvokeInternal();
        protected abstract void OnPhaseFinished(ActPhase phase);
    }

    private class ConsecutivePhasesInvoker : PhasesInvoker
    {
        private int _activePhaseIndex;

        public ConsecutivePhasesInvoker(List<ActPhase> _phases) : base(_phases) { }

        protected override void InvokeInternal()
        {
            _activePhaseIndex = -1;
            InvokeNextPhase();
        }

        private void InvokeNextPhase()
        {
            _activePhaseIndex++;
            if (_activePhaseIndex < Phases.Count)
            {
                Phases[_activePhaseIndex].Invoke();
            }
        }

        protected override void OnPhaseFinished(ActPhase phase)
        {
            if (phase != Phases[_activePhaseIndex])
            {
                return;
            }
            if (_activePhaseIndex + 1 >= Phases.Count)
            {
                InvokeFinished();
                return;
            }
            InvokeNextPhase();
        }
    }

    private class SimultaneousPhasesInvoker : PhasesInvoker
    {
        private readonly HashSet<ActPhase> _finishedPhases = new();

        public SimultaneousPhasesInvoker(List<ActPhase> _phases) : base(_phases) { }

        protected override void InvokeInternal()
        {
            _finishedPhases.Clear();
            Phases.ForEach(x => x.Invoke());
        }

        protected override void OnPhaseFinished(ActPhase phase)
        {
            _finishedPhases.Add(phase);
            if (Phases.All(x => _finishedPhases.Contains(x)))
            {
                InvokeFinished();
            }
        }
    }
    #endregion
}
