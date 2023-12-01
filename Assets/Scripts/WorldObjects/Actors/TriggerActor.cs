using UnityEngine;

[RequireComponent(typeof(WorldObjectTriggerDetector))]
public abstract class TriggerActor : EffectActor
{
    protected override void Awake()
    {
        base.Awake();
        var triggerDetector = GetComponent<WorldObjectTriggerDetector>();
        triggerDetector.TriggerEntered += Act;
        triggerDetector.TriggerExited += Cancel;
    }

    public abstract void Cancel(WorldObject worldObject);
}
