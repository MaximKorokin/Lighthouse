using System.Collections.Generic;

public class PeriodicActor : EffectActor
{
    public float Cooldown { get; set; }

    private readonly Dictionary<WorldObject, CooldownCounter> _cooldowns = new();

    protected override void ActInternal(WorldObject worldObject)
    {
        _cooldowns.TryAdd(worldObject, new CooldownCounter(Cooldown));
        if (_cooldowns[worldObject].TryReset(WorldObject.AttackSpeed))
        {
            base.ActInternal(worldObject);
        }
    }

    public override void Idle(WorldObject worldObject)
    {

    }
}