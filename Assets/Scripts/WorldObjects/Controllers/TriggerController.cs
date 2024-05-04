using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ValidatorTriggerDetector))]
public abstract class TriggerController : ControllerBase
{
    protected readonly HashSet<WorldObject> TriggeredWorldObjects = new();

    protected override void Awake()
    {
        base.Awake();

        var triggerDetector = GetComponent<ValidatorTriggerDetector>();
        triggerDetector.TriggerEntered += OnTriggerEntered;
        triggerDetector.TriggerExited += OnTriggerExited;
    }

    private void OnTriggerEntered(WorldObject worldObject)
    {
        if (worldObject is DestroyableWorldObject destroyable)
        {
            destroyable.OnDestroying(() => TriggeredWorldObjects.Remove(worldObject));
        }
        TriggeredWorldObjects.Add(worldObject);
        Trigger(worldObject, true);
    }

    private void OnTriggerExited(WorldObject worldObject)
    {
        TriggeredWorldObjects.Remove(worldObject);
        Trigger(worldObject, false);
    }

    protected abstract void Trigger(WorldObject worldObject, bool entered);
}
