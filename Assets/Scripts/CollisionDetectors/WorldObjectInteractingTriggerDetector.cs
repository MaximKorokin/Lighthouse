public class WorldObjectInteractingTriggerDetector : WorldObjectFindingTriggerDetector
{
    private WorldObject _worldObject;
    protected WorldObject WorldObject => gameObject.LazyGetComponent(ref _worldObject);

    protected override bool IsValidTarget(WorldObject obj, DetectingVariant variant)
    {
        if (WorldObject == null || (obj is DestroyableWorldObject destroyable && !destroyable.IsAlive))
        {
            return false;
        }

        return base.IsValidTarget(obj, variant)
            && (obj.PositioningType & WorldObject.TriggeringType) != PositioningType.None
            && variant.Relation.IsValidRelation(WorldObject, obj);
    }
}
