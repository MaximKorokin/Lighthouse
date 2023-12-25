using System.Collections.Generic;

public class PeriodicActor : EffectActor
{
    private readonly Dictionary<WorldObject, CooldownCounter> _cooldowns = new();

    protected override void ActInternal(WorldObject worldObject)
    {
        _cooldowns.TryAdd(worldObject, new CooldownCounter(CastState.Cooldown));
        if (_cooldowns[worldObject].TryReset(WorldObject.AttackSpeed))
        {
            base.ActInternal(worldObject);
        }
    }

    public override void Idle(WorldObject worldObject)
    {
        _cooldowns.Remove(worldObject);
    }
}