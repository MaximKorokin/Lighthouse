using UnityEngine;

public abstract class TriggerController : ControllerBase
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var worldObject = collision.GetComponent<WorldObject>();
        if (worldObject != null &&
            (worldObject.PositioningType & WorldObject.TriggeringType) != PositioningType.None &&
            Manipulator.IsValidTarget(worldObject))
        {
            Trigger(worldObject);
        }
    }

    protected abstract void Trigger(WorldObject worldObject);
}
