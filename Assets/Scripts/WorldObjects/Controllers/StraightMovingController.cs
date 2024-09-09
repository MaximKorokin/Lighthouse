using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StraightMovingController : TargetController
{
    public Vector2 Direction { get; set; }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        Direction = Quaternion.Euler(0, 0, yaw) * (worldObject.transform.position - transform.position);
    }

    public override void ChooseTarget(IEnumerable<WorldObject> targets, TargetSearchingType targetType, WorldObject source, float yaw)
    {
        targets = targets.Where(x => IsPrimaryTarget(x));
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
        MovableWorldObject.Direction = Direction.normalized;
        MovableWorldObject.Move();
    }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        base.Trigger(worldObject, entered);
        if (entered)
        {
            InvokeActors(new PrioritizedTargets(worldObject, TriggeredWorldObjects, PrimaryTargets, SecondaryTargets));
        }
    }
}
