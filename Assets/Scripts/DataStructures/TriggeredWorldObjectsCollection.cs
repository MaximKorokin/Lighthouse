using System;

public class TriggeredWorldObjectsCollection : TriggeredObjectsCollection<WorldObject>
{
    public TriggeredWorldObjectsCollection(TriggerDetectorBase<WorldObject> detector) : base(detector) { }
    public TriggeredWorldObjectsCollection(TriggerDetectorBase<WorldObject> detector, Func<WorldObject, bool> additionalCondition) : base(detector, additionalCondition) { }

    protected override void OnTriggerEntered(WorldObject worldObject)
    {
        if (worldObject is DestroyableWorldObject destroyable && !TriggeredObjects.Contains(destroyable))
        {
            destroyable.OnDestroying(() => TriggeredObjects.Remove(worldObject));
        }
        base.OnTriggerEntered(worldObject);
    }
}
