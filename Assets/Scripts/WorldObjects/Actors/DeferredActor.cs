using System.Linq;
using UnityEngine;

class DeferredActor : SkilledActor
{
    [SerializeField]
    private float _timeToAct;
    [SerializeField]
    private int _actsAmount;

    private Timer _timer;
    private int _actedAmount;
    private PrioritizedTargets _targets;

    protected override void Awake()
    {
        base.Awake();

        _timer = new Timer(this);
        _timer.Finished += OnTimerFinished;

        this.GetRequiredComponent<TimerVisualizer>().SetTimer(_timer);
    }

    private void OnTimerFinished()
    {
        foreach (var target in _targets.Targets)
        {
            if (++_actedAmount > _actsAmount)
            {
                break;
            }
            _targets.MainTarget = target;
            base.ActInternal(_targets);
        }
    }

    protected override void ActInternal(PrioritizedTargets targets)
    {
        if (_actedAmount >= _actsAmount)
        {
            return;
        }
        
        _targets = targets;

        if (_targets.Targets.Any())
        {
            if (!_timer.Started)
            {
                _timer.Start(_timeToAct);
            }
        }
        else if(_timer.Started)
        {
            _timer.Stop();
        }
    }
}
