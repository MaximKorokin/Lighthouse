using UnityEngine;

[RequireComponent(typeof(WorldObjectTriggerDetector))]
public abstract class TriggerController : ControllerBase
{
    protected override void Awake()
    {
        base.Awake();

        var triggerDetector = GetComponent<WorldObjectTriggerDetector>();
        triggerDetector.TriggerEntered += OnTriggerEntered;
        triggerDetector.TriggerExited += OnTriggerExited;
    }

    private void OnTriggerEntered(WorldObject worldObject)
    {
        Trigger(worldObject, true);
    }

    private void OnTriggerExited(WorldObject worldObject)
    {
        Trigger(worldObject, false);
    }

    protected abstract void Trigger(WorldObject worldObject, bool entered);
}
