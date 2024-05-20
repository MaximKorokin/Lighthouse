using UnityEngine;

public class WorldObjectTriggerDetector : TriggerDetectorBase<WorldObject>
{
    protected override bool ValidateTarget(Collider2D collision, out WorldObject worldObject)
    {
        if (!base.ValidateTarget(collision, out worldObject))
        {
            return false;
        }
        worldObject = collision.GetComponent<WorldObject>();
        return worldObject != null;
    }
}
