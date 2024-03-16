using UnityEngine;

public class StraightMovingController : TargetController
{
    public Vector2 Direction { get; set; }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        if (entered)
        {
            InvokeActors(worldObject);
        }
    }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        Direction = Quaternion.Euler(0, 0, yaw) * (worldObject.transform.position - transform.position);
    }

    public override void ChooseTarget(WorldObject[] targets, TargetSearchingType targetType, WorldObject source, float yaw)
    {
        Vector2 targetDirection = targetType switch
        {
            TargetSearchingType.Nearest => targets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude).transform.position - transform.position,
            TargetSearchingType.Random => Random.insideUnitCircle,
            TargetSearchingType.Forward => source is MovableWorldObject movableSource ? movableSource.Direction : Direction,
            _ => Direction
        };
        Direction = Quaternion.Euler(0, 0, yaw) * targetDirection;
    }

    protected override void Control()
    {
        MovableWorldObject.Direction = Direction;
        MovableWorldObject.Move();
    }
}
