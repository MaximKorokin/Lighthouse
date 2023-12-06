using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerVisualizer))]
class DeferredTriggerActor : TriggerActor
{
    [SerializeField]
    private float _timeToAct;
    [SerializeField]
    private int _actsAmount;

    private Timer _timer;
    private int _actedAmount;
    private readonly HashSet<WorldObject> worldObjects = new();

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
        worldObjects.ForEach(x => base.Act(x));
    }

    public override void Act(WorldObject worldObject)
    {
        if (_actedAmount >= _actsAmount)
        {
            return;
        }
        worldObjects.Add(worldObject);
        if (worldObjects.Count > 0 && !_timer.Started)
        {
            _timer.Start(_timeToAct);
        }
    }

    public override void Cancel(WorldObject worldObject)
    {
        if (_actedAmount >= _actsAmount)
        {
            return;
        }
        if (worldObjects.Contains(worldObject))
        {
            worldObjects.Remove(worldObject);
        }
        if (worldObjects.Count == 0)
        {
            _timer.Stop();
        }
    }
}
