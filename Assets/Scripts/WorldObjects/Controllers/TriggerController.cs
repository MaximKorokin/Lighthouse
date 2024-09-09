using UnityEngine;

[RequireComponent(typeof(WorldObjectInteractingTriggerDetector))]
public abstract class TriggerController : ControllerBase
{
    private TriggeredWorldObjectsCollection _triggeredWorldObjectsCollection;
    public IContainsEnumerable<WorldObject> TriggeredWorldObjects => _triggeredWorldObjectsCollection;

    private WorldObjectInteractingTriggerDetector _detector;
    public WorldObjectInteractingTriggerDetector Detector => _detector = _detector != null ? _detector : GetComponent<WorldObjectInteractingTriggerDetector>();

    protected override void Awake()
    {
        base.Awake();

        _triggeredWorldObjectsCollection = new TriggeredWorldObjectsCollection(Detector);
        _triggeredWorldObjectsCollection.Triggered += Trigger;
    }

    protected virtual void Trigger(WorldObject worldObject, bool entered) { }
}
