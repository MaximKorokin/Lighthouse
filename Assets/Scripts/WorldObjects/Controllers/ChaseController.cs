using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaseController : TargetController
{
    private readonly CooldownCounter _targetSwitchAttemptCooldown = new(1.5f);

    public WorldObject Target { get; private set; }

    protected override void Trigger(WorldObject worldObject, bool entered) { }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        if (Target == null)
        {
            Target = worldObject;
        }
    }

    public override void ChooseTarget(ICollection<WorldObject> targets, TargetSearchingType targetType, WorldObject source, float yaw)
    {
        Target = targetType switch
        {
            TargetSearchingType.Nearest => targets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude),
            TargetSearchingType.Random => targets.Skip(Random.Range(0, targets.Count - 1)).First(),
            _ => targets.First(),
        };
    }

    protected override void Control()
    {
        SeekNearestTarget();
        if (Target == null)
        {
            MovableWorldObject.Stop();
            return;
        }
        var direction = (Vector2)Target.transform.position - (Vector2)transform.position;
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

    private void SeekNearestTarget()
    {
        if (TriggeredWorldObjects.Count > 0 && (_targetSwitchAttemptCooldown.TryReset() || !TriggeredWorldObjects.Contains(Target)))
        {
            ChooseTarget(TriggeredWorldObjects, TargetSearchingType.Nearest, WorldObject, 0);
        }
    }
}
