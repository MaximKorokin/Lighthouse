using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaseController : TargetController
{
    private readonly CooldownCounter _targetSwitchAttemptCooldown = new(1.5f);

    public WorldObject Target { get; private set; }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        Target = worldObject;
    }

    public override void ChooseTarget(IEnumerable<WorldObject> targets, TargetSearchingType targetType, WorldObject source, float yaw)
    {
        targets = targets.Where(x => IsPrimaryTarget(x));
        SetTarget(targetType switch
        {
            TargetSearchingType.Nearest => targets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude),
            TargetSearchingType.Random => targets.Skip(Random.Range(0, targets.Count() - 1)).First(),
            _ => targets.First(),
        }, yaw);
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

        if (direction.magnitude > WorldObject.ActionRange)
        {
            MovableWorldObject.Move();
        }
        else
        {
            MovableWorldObject.Stop();
        }
        InvokeActors(new PrioritizedTargets(Target, TriggeredWorldObjects, PrimaryTargets, SecondaryTargets));
    }

    private void SeekNearestTarget()
    {
        if (TriggeredWorldObjects.Any() && (_targetSwitchAttemptCooldown.TryReset() || !TriggeredWorldObjects.Contains(Target)))
        {
            ChooseTarget(TriggeredWorldObjects, TargetSearchingType.Nearest, WorldObject, 0);
        }
    }
}
