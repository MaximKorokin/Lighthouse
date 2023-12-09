using System.Collections.Generic;

public class PeriodicActor : EffectActor
{
    private readonly Dictionary<WorldObject, CooldownCounter> _cooldowns = new();

    public override void Act(WorldObject worldObject)
    {
        _cooldowns.TryAdd(worldObject, new CooldownCounter(Cooldown));
        if (_cooldowns[worldObject].TryReset(WorldObject.AttackSpeed))
        {
            base.Act(worldObject);
        }
    }

    public override void Idle(WorldObject worldObject)
    {
        _cooldowns.Remove(worldObject);
    }
}