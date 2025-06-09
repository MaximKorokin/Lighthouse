using System.Collections.Generic;

public class PeriodicActor : SkilledActor
{
    public float Cooldown { get; set; }

    private readonly Dictionary<WorldObject, CooldownCounter> _cooldowns = new();

    protected override void ActInternal(PrioritizedTargets targets)
    {
        foreach (var target in targets.Targets)
        {
            _cooldowns.TryAdd(target, new CooldownCounter(Cooldown));
            _cooldowns[target].CooldownDivider = WorldObject.AttackSpeed;
            if (_cooldowns[target].TryReset())
            {
                targets.MainTarget = target;
                base.ActInternal(targets);
            }
        }
    }
}
