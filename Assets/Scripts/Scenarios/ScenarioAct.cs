using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ScenarioAct : MonoBehaviour, IInitializable<ScenarioAct>
{
    [SerializeField]
    private List<ActRequirement> _requirements;
    [SerializeField, HideInInspector]
    private List<ActPhase> _phases;
    [SerializeField]
    private List<ScenarioAct> _childrenActs;

    [SerializeField]
    private PhasesInvokationType _phasesInvokationType = PhasesInvokationType.Consecutive;

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
        if (!_hasInitialized)
        {
            _invoker = _phasesInvokationType switch
            {
                PhasesInvokationType.Consecutive => new ConsecutivePhasesInvoker(_phases),
                PhasesInvokationType.Simultaneous => new SimultaneousPhasesInvoker(_phases),
                PhasesInvokationType.RandomSingle => new RandomSinglePhasesInvoker(_phases),
                _ => null,
            };
            _invoker.Finished += OnFinished;
            _hasInitialized = true;

            Initialized?.Invoke(this);
        }

        foreach (var requirement in _requirements)
        {
            requirement.OnFulfilled -= OnRequirementFulfilled;
            requirement.OnFulfilled += OnRequirementFulfilled;
        }

        if (_requirements.Count == 0 || _requirements.All(x => x.IsFulfilled()))
        {
            _invoker.Invoke();
        }
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
    private enum PhasesInvokationType
    {
        Consecutive = 10,
        Simultaneous = 20,
        RandomSingle = 30,
    }

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

    private class RandomSinglePhasesInvoker : PhasesInvoker
    {
        public RandomSinglePhasesInvoker(List<ActPhase> _phases) : base(_phases) { }

        protected override void InvokeInternal()
        {
            Phases[UnityEngine.Random.Range(0, Phases.Count)].Invoke();
        }

        protected override void OnPhaseFinished(ActPhase phase)
        {
            InvokeFinished();
        }
    }
    #endregion
}
