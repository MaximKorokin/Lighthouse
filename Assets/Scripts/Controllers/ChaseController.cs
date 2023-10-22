using UnityEngine;

public class ChaseController : TriggerController
{
    [field: SerializeField]
    public WorldObject Target { get; set; }

    protected override void Trigger(WorldObject worldObject)
    {
        if (Target == null)
        {
            Target = worldObject;
        }
    }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        Target = worldObject;
    }

    protected override void ChooseTarget(WorldObject[] targets, TargetType targetType, WorldObject source, float yaw)
    {
        Target = targetType switch
        {
            TargetType.Nearest => targets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude),
            TargetType.Random => targets[Random.Range(0, targets.Length)],
            _ => targets[0]
        };
    }

    protected override void Control()
    {
        if (Target == null)
        {
            return;
        }
        var direction = Target.transform.position - transform.position;
        // sqrt is much slower than sqr
        if (direction.sqrMagnitude > WorldObject.ActionRange * WorldObject.ActionRange)
        {
            WorldObject.Move(direction);
        }
        else
        {
            WorldObject.Act(Target);
            WorldObject.Stop();
        }
    }
}
