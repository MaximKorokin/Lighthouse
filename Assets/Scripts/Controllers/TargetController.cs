using UnityEngine;

public abstract class TargetController : TriggerController
{
    public abstract void ChooseTarget(WorldObject[] targets, TargetType targetType, WorldObject source, float yaw);

    public abstract void SetTarget(WorldObject worldObject, float yaw);
}
