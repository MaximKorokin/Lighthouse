using UnityEngine;

public abstract class TriggerController : ControllerBase
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (ValidateTrigger(collision, out var worldObject))
        {
            Trigger(worldObject, true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (ValidateTrigger(collision, out var worldObject))
        {
            Trigger(worldObject, false);
        }
    }

    protected abstract void Trigger(WorldObject worldObject, bool entered);

    private bool ValidateTrigger(Collider2D collision, out WorldObject worldObject)
    {
        if (collision.isTrigger)
        {
            worldObject = null;
            return false;
        }
        worldObject = collision.GetComponent<WorldObject>();
        return worldObject != null &&
            (worldObject.PositioningType & WorldObject.TriggeringType) != PositioningType.None &&
            Validator.IsValidTarget(worldObject);
    }
}
