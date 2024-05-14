using UnityEngine;

[RequireComponent(typeof(WorldObject))]
[RequireComponent(typeof(WorldObjectValidator))]
public class ValidatingTriggerDetector : WorldObjectTriggerDetector
{
    [SerializeField]
    private FactionsRelation _triggerOn;

    protected WorldObject WorldObject { get; private set; }
    protected WorldObjectValidator Validator { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<WorldObject>();
        Validator = GetComponent<WorldObjectValidator>();
    }

    protected override bool ValidateTarget(Collider2D collision, out WorldObject worldObject)
    {
        if (!base.ValidateTarget(collision, out worldObject) || WorldObject == null)
        {
            return false;
        }
        return (worldObject.PositioningType & WorldObject.TriggeringType) != PositioningType.None &&
            Validator.IsValidTarget(worldObject, _triggerOn);
    }
}
