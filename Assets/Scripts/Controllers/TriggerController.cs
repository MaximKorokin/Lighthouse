using UnityEngine;

public abstract class TriggerController : ControllerBase
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }
        var worldObject = collision.GetComponent<WorldObject>();
        if (worldObject != null &&
            (worldObject.PositioningType & WorldObject.TriggeringType) != PositioningType.None &&
            Validator.IsValidTarget(worldObject))
        {
            Trigger(worldObject);
        }
    }

    protected abstract void Trigger(WorldObject worldObject);
}
