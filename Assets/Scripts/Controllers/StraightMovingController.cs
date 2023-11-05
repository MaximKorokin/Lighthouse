using UnityEngine;

public class StraightMovingController : TargetController
{
    [field: SerializeField]
    public Vector2 Direction { get; set; }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        if (Validator.IsValidTarget(worldObject) && entered)
        {
            WorldObject.Act(worldObject);
        }
    }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        Direction = Quaternion.Euler(0, 0, yaw) * (worldObject.transform.position - transform.position);
    }

    public override void ChooseTarget(WorldObject[] targets, TargetType targetType, WorldObject source, float yaw)
    {
        Vector2 targetDirection = targetType switch
        {
            TargetType.Nearest => targets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude).transform.position - transform.position,
            TargetType.Random => Random.insideUnitCircle,
            TargetType.Forward => source is MovableWorldObject movableSource ? movableSource.Direction : Direction,
            _ => Direction
        };
        Direction = Quaternion.Euler(0, 0, yaw) * targetDirection;
    }

    protected override void Control()
    {
        WorldObject.Move(Direction);
    }
}
