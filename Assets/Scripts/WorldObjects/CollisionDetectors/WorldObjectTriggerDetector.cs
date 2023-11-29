using UnityEngine;

[RequireComponent(typeof(WorldObject))]
[RequireComponent(typeof(ValidatorBase))]
public class WorldObjectTriggerDetector : TriggerDetectorBase<WorldObject>
{
    protected MovableWorldObject WorldObject { get; private set; }
    protected ValidatorBase Validator { get; private set; }

    protected virtual void Start()
    {
        WorldObject = GetComponent<MovableWorldObject>();
        Validator = GetComponent<ValidatorBase>();
    }

    protected override bool ValidateTarget(Collider2D collision, out WorldObject worldObject)
    {
        if (!base.ValidateTarget(collision, out worldObject))
        {
            return false;
        }
        worldObject = collision.GetComponent<WorldObject>();
        return worldObject != null &&
            (worldObject.PositioningType & WorldObject.TriggeringType) != PositioningType.None &&
            Validator.IsValidTarget(worldObject);
    }
}
