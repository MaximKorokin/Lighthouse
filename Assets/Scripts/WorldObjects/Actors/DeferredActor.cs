using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerVisualizer))]
class DeferredActor : EffectActor
{
    [SerializeField]
    private float _timeToAct;
    [SerializeField]
    private int _actsAmount;

    private Timer _timer;
    private int _actedAmount;
    private readonly HashSet<WorldObject> _worldObjects = new();

    protected override void Awake()
    {
        base.Awake();

        _timer = new Timer(this);
        _timer.Finished += OnTimerFinished;

        GetComponent<TimerVisualizer>().SetTimer(_timer);
    }

    private void OnTimerFinished()
    {
        _actedAmount++;
        base.ActInternal(WorldObject);
    }

    protected override void ActInternal(WorldObject worldObject)
    {
        if (_actedAmount >= _actsAmount)
        {
            return;
        }
        _worldObjects.Add(worldObject);
        if (!_timer.Started)
        {
            _timer.Start(_timeToAct);
        }
    }

    public override void Idle(WorldObject worldObject)
    {
        if (_actedAmount >= _actsAmount)
        {
            return;
        }
        _worldObjects.Remove(worldObject);
        if (_worldObjects.Count == 0)
        {
            _timer.Stop();
        }
    }
}
