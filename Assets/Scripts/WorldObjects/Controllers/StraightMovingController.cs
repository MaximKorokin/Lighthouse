using System.Collections.Generic;
using System.Linq;
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

    public override void ChooseTarget(IEnumerable<WorldObject> targets, TargetSearchingType targetType, WorldObject source, float yaw)
    {
        targets = targets.Where(x => IsValidTarget(x, HighPriorityIndex));
        Vector2 targetDirection = targetType switch
        {
            TargetSearchingType.Nearest => targets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude).transform.position - transform.position,
            TargetSearchingType.Random => Random.insideUnitCircle,
            TargetSearchingType.Forward => source is MovableWorldObject movableSource ? movableSource.Direction : Direction,
            _ => Direction
        };
        Direction = targetDirection.Rotate(yaw);
    }

    protected override void Control()
    {
        MovableWorldObject.Direction = Direction;
        MovableWorldObject.Move();
    }
}
