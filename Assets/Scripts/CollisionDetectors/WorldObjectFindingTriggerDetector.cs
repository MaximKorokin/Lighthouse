using UnityEngine;

public class WorldObjectFindingTriggerDetector : TriggerDetectorBase<WorldObject>
{
    protected override bool IsValidTarget(WorldObject obj, DetectingVariant variant)
    {
        return variant.ValidTargets.IsValidTarget(obj);
    }

    protected override bool TryGetTargetingObject(Collider2D collision, out WorldObject worldObject)
    {
        return collision.TryGetComponent(out worldObject);
    }
}
