using UnityEngine;

public class ChaseController : TargetController
{
    public WorldObject Target { get; private set; }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        if (entered)
        {
            SetTarget(worldObject, 0);
        }
    }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        if (Target == null)
        {
            Target = worldObject;
        }
    }

    public override void ChooseTarget(WorldObject[] targets, TargetType targetType, WorldObject source, float yaw)
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
            MovableWorldObject.Stop();
            return;
        }
        var direction = Target.transform.position - transform.position;
        MovableWorldObject.Direction = direction.normalized;
        // sqrt is much slower than sqr
        if (direction.sqrMagnitude > WorldObject.ActionRange * WorldObject.ActionRange)
        {
            MovableWorldObject.Move();
        }
        else
        {
            InvokeActors(Target);
            MovableWorldObject.Stop();
        }
    }
}
