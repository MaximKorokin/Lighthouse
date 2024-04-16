using System.Collections.Generic;
using UnityEngine;

public class ChaseController : TargetController
{
    private readonly CooldownCounter _targetSwitchAttemptCooldown = new(1.5f);
    private readonly List<WorldObject> _potentialTargets = new();

    public WorldObject Target { get; private set; }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        if (entered)
        {
            if (worldObject is DestroyableWorldObject destroyable)
            {
                destroyable.OnDestroying(() => _potentialTargets.Remove(worldObject));
            }
            _potentialTargets.Add(worldObject);
        }
        else
        {
            _potentialTargets.Remove(worldObject);
        }
    }

    public override void SetTarget(WorldObject worldObject, float yaw)
    {
        if (Target == null)
        {
            Target = worldObject;
        }
    }

    public override void ChooseTarget(IList<WorldObject> targets, TargetSearchingType targetType, WorldObject source, float yaw)
    {
        Target = targetType switch
        {
            TargetSearchingType.Nearest => targets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude),
            TargetSearchingType.Random => targets[Random.Range(0, targets.Count)],
            _ => targets[0]
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
        if (_potentialTargets.Count > 0 && (_targetSwitchAttemptCooldown.TryReset() || !_potentialTargets.Contains(Target)))
        {
            ChooseTarget(_potentialTargets, TargetSearchingType.Nearest, WorldObject, 0);
        }
    }
}
