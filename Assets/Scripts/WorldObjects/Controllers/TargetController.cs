using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TargetController : MovableController
{
    [SerializeField]
    private ValidTarget _primaryTargetTypes;

    public WorldObject Target { get; set; }

    protected IEnumerable<WorldObject> PrimaryTargets => TriggeredWorldObjects.Where(IsPrimaryTarget);
    protected IEnumerable<WorldObject> SecondaryTargets => TriggeredWorldObjects.Except(PrimaryTargets);

    private readonly CooldownCounter _targetSwitchAttemptCooldown = new(1.5f);

    public bool IsPrimaryTarget(WorldObject worldObject)
    {
        return _primaryTargetTypes.IsValidTarget(worldObject);
    }

    protected override void Control()
    {
        if (!Detector.IsValidTarget(Target) || _targetSwitchAttemptCooldown.TryReset() || !TriggeredWorldObjects.Contains(Target))
        {
            Target = GetNearestTarget();
        }
    }

    private WorldObject GetNearestTarget()
    {
        WorldObject target = null;
        if (TriggeredWorldObjects.Any())
        {
            var primaryTargets = TriggeredWorldObjects.Where(x => IsPrimaryTarget(x)).ToArray();
            if (primaryTargets.Any())
            {
                target = primaryTargets.MinBy(w => (w.transform.position - transform.position).sqrMagnitude);
            }
        }
        return target;
    }
}
