using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ValidatingTriggerDetector))]
public abstract class TriggerController : ControllerBase
{
    private TriggeredWorldObjectsCollection _triggeredWorldObjectsCollection;
    protected IEnumerable<WorldObject> TriggeredWorldObjects => _triggeredWorldObjectsCollection;

    protected override void Awake()
    {
        base.Awake();

        var triggerDetector = GetComponent<ValidatingTriggerDetector>();
        _triggeredWorldObjectsCollection = new TriggeredWorldObjectsCollection(triggerDetector);
        _triggeredWorldObjectsCollection.Triggered += Trigger;
    }

    protected abstract void Trigger(WorldObject worldObject, bool entered);
}
