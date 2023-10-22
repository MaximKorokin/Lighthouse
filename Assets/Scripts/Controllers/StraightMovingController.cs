using UnityEngine;

public class StraightMovingController : TriggerController
{
    [field: SerializeField]
    public Vector2 Direction { get; set; }

    protected override void Trigger(WorldObject worldObject)
    {
        if (Validator.IsValidTarget(worldObject))
        {
            WorldObject.Act(worldObject);
        }
    }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        Direction = Quaternion.Euler(0, 0, yaw) * (transform.position - worldObject.transform.position);
    }

    protected override void ChooseTarget(WorldObject[] targets, TargetType targetType, WorldObject source, float yaw)
    {
        Vector2 targetDirection = targetType switch
        {
            TargetType.Nearest => targets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude).transform.position - transform.position,
            //                   Random.insideUnitCircle
            TargetType.Random => targets[Random.Range(0, targets.Length)].transform.position,
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
